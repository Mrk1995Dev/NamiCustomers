using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.SevenSoftServices;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Infrastucture.Model.Subscribers;
using NamiCustomers.Infrastucture.Utilities;
using System.Text;

namespace NamiCustomers.Application.Services.Subscribers;

public interface ISubscriberService
{
    Task<ResultDto<List<SubscriberDto>>> GetAllAsync();
    Task<ResultDto> RegisterAsync(SubscriberDto subscriberDto);
    Task<ResultDto> DeleteAsync(int id);
    Task<ResultDto> EditAsync(SubscriberDto updateCustomerInfoDto);
    Task<ResultDto<SubscriberDto>> GetAsync(int id);
    Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync(string nationalCode);
    Task<ResultDto<SubscriberDto>> GetAsync(string mobile);
    Task<ResultDto<byte[]>> ExportAsync();
    Task<List<CityDto>> GetCitiesAsync();
    Task<ResultDto<SubscriberCodeDto>> SendOtpAsync(string mobile);
    Task<ResultDto<SubscriberCodeDto>> GetOtpAsync(string mobile, string nationalCode);
}
public class SubscriberService(IAppDbContext dbContext, ISmsService smsService, ISevenSoftService sevenSoftService) : ISubscriberService
{
    public async Task<ResultDto> RegisterAsync(SubscriberDto addCustomerInfoDto)
    {
        if (addCustomerInfoDto == null) return new ResultDto("اطلاعات وارد شده نامعتبر می باشد.", false);
        Subscriber newCustomer = new Subscriber
        {
            Name = addCustomerInfoDto.Name,
            Address = addCustomerInfoDto.Address,
            CityId = addCustomerInfoDto.CityId,
            Mobile = addCustomerInfoDto.PhoneNumber,
        };

        await dbContext.Subscribers.AddAsync(newCustomer);
        if (await dbContext.SaveChangesAsync() < 1) return new ResultDto("خطا در ذخیره اطلاعات مربوطه", false);

        return new ResultDto("اطلاعات با موفقیت ذخیره شد.", true);
    }

    public async Task<ResultDto> DeleteAsync(int customerId)
    {
        if (customerId == 0)
            return new ResultDto("شناسه وارد شده نامعتبر می باشد.", false);

        var customer = await dbContext.Subscribers.FirstOrDefaultAsync(cu => cu.Id == customerId);
        if (customer is null)
            return new ResultDto("کاربر مربوطه یافت نشد.", false);

        dbContext.Subscribers.Remove(customer);
        if (await dbContext.SaveChangesAsync() < 1) return new ResultDto("خطا در حذف اطلاعات مربوطه", false);

        return new ResultDto("کاربر با موفقیت حذف شد.", true);
    }

    public async Task<ResultDto<List<SubscriberDto>>> GetAllAsync()
    {
        var customers = dbContext.Subscribers.AsQueryable();

        var data = await customers.Select(c => new SubscriberDto
        {
            Id = c.Id,
            Name = c.Name,
            Address = c.Address,
            CityName = c.City.Title,
            PhoneNumber = c.Mobile
        }).ToListAsync();

        return new ResultDto<List<SubscriberDto>>(
            "",
            true,
            data);
    }

    public async Task<ResultDto> EditAsync(SubscriberDto subscriberDto)
    {
        var currentCustomer = await dbContext.Subscribers.Where(cu => cu.Id == subscriberDto.Id).FirstOrDefaultAsync();
        if (currentCustomer is null)
            return new ResultDto(
                "کاربر مربوطه یافت نشد",
                false);

        currentCustomer.Name = subscriberDto.Name;
        currentCustomer.Address = subscriberDto.Address;
        currentCustomer.Mobile = subscriberDto.PhoneNumber;
        currentCustomer.CityId = subscriberDto.CityId;

        dbContext.Subscribers.Update(currentCustomer);
        if (await dbContext.SaveChangesAsync() < 1)
            return new ResultDto(
                "خطا در ویرایش اطلاعات کاربر",
                false);

        return new ResultDto(
            "اطلاعات کاربر با موفقیت ویرایش شد",
            true);
    }


    public async Task<ResultDto<SubscriberDto>> GetAsync(int id)
    {

        var data = await dbContext.Subscribers.Where(cu => cu.Id == id)
            .Include(cu => cu.City).FirstOrDefaultAsync();

        if (data == null) return new ResultDto<SubscriberDto>(
            "کاربر مربوطه یافت نشد.",
            false,
            null);

        var customerInfo = new SubscriberDto
        {
            Id = data.Id,
            Name = data.Name,
            Address = data.Address,
            //CityName = data.City.Title,
            PhoneNumber = data.Mobile,
        };

        return new ResultDto<SubscriberDto>(
            "",
            true,
            customerInfo);
    }


    public async Task<ResultDto<SubscriberDto>> GetAsync(string mobile)
    {

        var data = await dbContext.Subscribers.Where(cu => cu.Mobile == mobile)
            .Include(cu => cu.City).FirstOrDefaultAsync();

        if (data == null) return new ResultDto<SubscriberDto>(
            "کاربر مربوطه یافت نشد.",
            false,
            null);

        var customerInfo = new SubscriberDto
        {
            Id = data.Id,
            Name = data.Name,
            Address = data.Address,
            CityName = data.City.Title,
            PhoneNumber = data.Mobile,
        };

        return new ResultDto<SubscriberDto>(
            "",
            true,
            customerInfo);
    }

    public async Task<ResultDto<byte[]>> ExportAsync()
    {
        var customerInfos = await dbContext.Subscribers
            .Include(c => c.City)
            .ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("Id\tName\tAddress\tCityName\tPhoneNumber\tFax");

        foreach (var item in customerInfos)
        {
            sb.AppendLine($"{item.Id}\t{item.Name}\t{item.Address}\t{item.City.Title}\t{item.Mobile}");
        }

        var bytes = Encoding.UTF8.GetBytes(sb.ToString());

        return new ResultDto<byte[]>(
            "خروجی با موفقیت دانلود شد",
            true,
            bytes);
    }

    public async Task<List<CityDto>> GetCitiesAsync()
    {
        var cities = await dbContext.Cities.ToListAsync();

        var data = cities.Select(c => new CityDto
        {
            Id = c.Id,
            Title = c.Title
        }).ToList();

        return data;
    }

    public async Task<ResultDto<SubscriberCodeDto>> SendOtpAsync(string authCode)
    {
        var otp = await dbContext.SubscriberCodes.FirstOrDefaultAsync(c => !c.Used && c.AuthCode == authCode);
        if (otp is null)
        {
            return new ResultDto<SubscriberCodeDto>("Not found !", false, new SubscriberCodeDto());
        }

        otp.Used = true;
        await dbContext.SaveChangesAsync();
        return new ResultDto<SubscriberCodeDto>("", true, new SubscriberCodeDto { AuthCode = otp.AuthCode, Mobile = otp.Mobile });
    }

    public async Task<ResultDto<SubscriberCodeDto>> GetOtpAsync(string mobile, string nationalCode)
    {
        var Randowmpass = new PasswordUtility();
        string passnew = Randowmpass.RandomString(5);
        var newOtp = new SubscriberCode { AuthCode = passnew, Mobile = mobile };
        await dbContext.SubscriberCodes.AddAsync(newOtp);
        await dbContext.SaveChangesAsync();
        var result = await smsService.SendSms(newOtp.Mobile, $"{newOtp.AuthCode}\n لغو11");
        return new ResultDto<SubscriberCodeDto>("", result.IsSuccessStatusCode, new SubscriberCodeDto { AuthCode = newOtp.AuthCode, Mobile = newOtp.Mobile, NationalCode = nationalCode });
    }

    public async Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync(string nationalCode)
    {
        var subscriber = await dbContext.Subscribers.Where(cu => cu.NationalCode == nationalCode)
            .Include(cu => cu.VehicleModels).FirstOrDefaultAsync();

        if (subscriber is null)
        {
            var sevenMember = await sevenSoftService.GetSubscriberByNationalCode(nationalCode);
            if (sevenMember is null)
            {
                return new ResultDto<SubscriberDto>(
            "کاربر مربوطه یافت نشد.",
            false,
            null);
            }
            else
            {
                var newSubscriber = new Subscriber
                {
                    Name = sevenMember.Name,
                    Address = sevenMember.Address,
                    BrithDate = sevenMember.BirthDate,
                    BrithDatePersian = sevenMember.StrBirthDate,
                    FathersName = sevenMember.FatherName,
                    IdNumber = sevenMember.IdNumber,
                    EconomicCode = sevenMember.EconomicCode?.ToString(),
                    Phone = sevenMember.Tel,
                    NationalCode = sevenMember.NationalCode,
                    Family = sevenMember.LastName,
                    Mobile = sevenMember.Mobile,
                    Sex = sevenMember.Gender == 1 ? "زن" : "مرد",
                };
                //if (sevenMember.VinNumber != null)
                //{
                //    var chassisInformation = await sevenSoftService.GetChassisInformationByVinNumber(sevenMember.VinNumber.ToString());
                //    if (chassisInformation != null)
                //    {
                //        newSubscriber.VehicleModels = new List<VehicleModel>
                //        {
                //            new VehicleModel
                //            {
                //                EnglishName = chassisInformation.VehicleModelName,
                //                BrandIdSevenSoft = (Guid?)chassisInformation.BrandId,
                //                //Description =chassisInformation.des,
                //                //SaleBasketIdSevenSoft =(Guid?) chassisInformation,
                //                //SalePlanIdSevenSoft = chassisInformation.pla,

                //                VehicleModelIdSevensoft = new Guid(chassisInformation.VehicleModelId),
                //                VehicleName = chassisInformation.VehicleModelName,
                //                VinNumber = chassisInformation.VinNumber,
                //            }
                //        };
                //    }

                //    dbContext.Subscribers.Add(newSubscriber);
                //    await dbContext.SaveChangesAsync();
                //}
            }
            var data = await dbContext.Subscribers.Where(cu => cu.NationalCode == nationalCode)
               .Include(cu => cu.City).FirstOrDefaultAsync();
            var customerInfo = new SubscriberDto
            {
                Id = data.Id,
                Name = data.Name,
                Address = data.Address,

                PhoneNumber = data.Phone,
                NationalCode = data.NationalCode,
                Family = data.Family,
                Mobile = data.Mobile,
                Sex = data.Sex,
                VehicleModels = subscriber.VehicleModels.Any() ? data.VehicleModels.Select(c => new VehicleModelDto
                {
                    EnglishName = c.EnglishName,
                    BrandIdSevenSoft = c.BrandIdSevenSoft,
                    Description = c.Description,
                    SaleBasketIdSevenSoft = c.SaleBasketIdSevenSoft,
                    SalePlanIdSevenSoft = c.SalePlanIdSevenSoft,
                    SubscriberId = c.SubscriberId,
                    VehicleModelIdSevensoft = c.VehicleModelIdSevensoft,
                    VehicleName = c.VehicleName,
                    VinNumber = c.VinNumber,
                }).ToList() : new()
            };

            return new ResultDto<SubscriberDto>(
                "",
                true,
                customerInfo);
        }
        else
        {
            var customerInfo = new SubscriberDto
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                Address = subscriber.Address,

                PhoneNumber = subscriber.Phone,
                NationalCode = subscriber.NationalCode,
                Family = subscriber.Family,
                Mobile = subscriber.Mobile,
                Sex = subscriber.Sex,
                VehicleModels = subscriber.VehicleModels.Any() ? subscriber.VehicleModels.Select(c => new VehicleModelDto
                {
                    EnglishName = c.EnglishName,
                    BrandIdSevenSoft = c.BrandIdSevenSoft,
                    Description = c.Description,
                    SaleBasketIdSevenSoft = c.SaleBasketIdSevenSoft,
                    SalePlanIdSevenSoft = c.SalePlanIdSevenSoft,
                    SubscriberId = c.SubscriberId,
                    VehicleModelIdSevensoft = c.VehicleModelIdSevensoft,
                    VehicleName = c.VehicleName,
                    VinNumber = c.VinNumber,
                }).ToList() : new()
            };
            return new ResultDto<SubscriberDto>(
      "",
      true,
      customerInfo);
        }


    }
}
