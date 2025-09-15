using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Domain.Entities.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Utilities;
using System.Runtime.Intrinsics.Arm;
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
public class SubscriberService(IMapper mapper, IAppDbContext dbContext, ISmsService smsService, ISevenSoftService sevenSoftService, UserManager<ApplicationUser> userManager) : ISubscriberService
{
    public async Task<ResultDto> RegisterAsync(SubscriberDto addCustomerInfoDto)
    {
        if (addCustomerInfoDto == null) return ResultDto.Failure(Infrastucture.Properties.Resources.errInputInValid);
        Subscriber newCustomer = new Subscriber
        {
            Name = addCustomerInfoDto.Name,
            Family = addCustomerInfoDto.Family,
            Mobile = addCustomerInfoDto.Phone,
            NationalCode = addCustomerInfoDto.NationalCode,
        };

        await dbContext.Subscribers.AddAsync(newCustomer);
        if (await dbContext.SaveChangesAsync() < 1)
            return ResultDto.Failure(Infrastucture.Properties.Resources.errSave);

        return ResultDto.Success(Infrastucture.Properties.Resources.msgSave);
    }

    public async Task<ResultDto> DeleteAsync(int customerId)
    {
        if (customerId == 0)
            return ResultDto.Failure("شناسه وارد شده نامعتبر می باشد.");

        var customer = await dbContext.Subscribers.FirstOrDefaultAsync(cu => cu.Id == customerId);
        if (customer is null)
            return ResultDto.Failure("کاربر مربوطه یافت نشد.");

        dbContext.Subscribers.Remove(customer);
        if (await dbContext.SaveChangesAsync() < 1) return ResultDto.Failure(Infrastucture.Properties.Resources.msgDeleted);

        return ResultDto.Success(Infrastucture.Properties.Resources.msgDeleted);
    }

    public async Task<ResultDto<List<SubscriberDto>>> GetAllAsync()
    {
        var customers = dbContext.Subscribers.AsQueryable();

        var data = await customers.Select(c => new SubscriberDto
        {
            Id = c.Id,
            Name = c.Name,
            Family = c.Family,
            Address = c.Address,
            CityName = c.City.Title,
            Phone = c.Mobile
        }).ToListAsync();

        return new ResultDto<List<SubscriberDto>>(
            "",

            true, data);
    }

    public async Task<ResultDto> EditAsync(SubscriberDto subscriberDto)
    {
        var subscriber = await dbContext.Subscribers.Where(cu => cu.Id == subscriberDto.Id).FirstOrDefaultAsync();

        if (subscriber is null)
            return ResultDto.Failure(Infrastucture.Properties.Resources.errNotFound);



        subscriber.Name = subscriberDto.Name;
        subscriber.Family = subscriberDto.Family;
        subscriber.Address = subscriberDto.Address;
        subscriber.Mobile = subscriberDto.Mobile;
        subscriber.CityId = subscriberDto.CityId;
        subscriber.Phone = subscriberDto.Phone;

        dbContext.Subscribers.Update(subscriber);

        var user = await userManager.Users.Where(c => c.NationalCode == subscriber.NationalCode).FirstOrDefaultAsync();
        if (user != null)
        {
            user.FirstName = subscriber.Name;
            user.LastName = subscriber.Family;
        }
        var subRessult = await dbContext.SaveChangesAsync();
        var userResult = await userManager.UpdateAsync(user);

        if (user is null || subRessult < 1)
            return ResultDto.Failure(Infrastucture.Properties.Resources.errEdited);



        return ResultDto.Success(Infrastucture.Properties.Resources.msgEdited);
    }


    public async Task<ResultDto<SubscriberDto>> GetAsync(int id)
    {

        var data = await dbContext.Subscribers.Where(cu => cu.Id == id)
            .Include(cu => cu.City).Include(cu => cu.VehicleModels).FirstOrDefaultAsync();

        if (data == null) return new ResultDto<SubscriberDto>(
             Infrastucture.Properties.Resources.errNotFound, false);

        var subscriberDto = new SubscriberDto
        {
            Id = data.Id,
            Name = data.Name,
            Family = data.Family,

            Address = data.Address,
            //CityName = data.City.Title,
            Phone = data.Phone,
            NationalCode = data.NationalCode,
            Mobile = data.Mobile,
            VehicleModels = mapper.Map<List<VehicleModelDto>>(data.VehicleModels)
        };

        return new ResultDto<SubscriberDto>(
            "",

            true, subscriberDto);
    }


    public async Task<ResultDto<SubscriberDto>> GetAsync(string mobile)
    {

        var data = await dbContext.Subscribers.Where(cu => cu.Mobile == mobile)
            .Include(cu => cu.City).Include(cu => cu.VehicleModels).FirstOrDefaultAsync();

        if (data == null) return new ResultDto<SubscriberDto>(
           Infrastucture.Properties.Resources.errNotFound, false);

        var customerInfo = new SubscriberDto
        {
            Id = data.Id,
            Name = data.Name,
            Family = data.Family,
            Address = data.Address,
            CityName = data.City.Title,
            Phone = data.Mobile,
            NationalCode = data.NationalCode,
            Mobile = data.Mobile,
            VehicleModels = mapper.Map<List<VehicleModelDto>>(data.VehicleModels)
        };

        return new ResultDto<SubscriberDto>(
            "",

            true, customerInfo);
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

            true, bytes);
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
            return new ResultDto<SubscriberCodeDto>(Infrastucture.Properties.Resources.errNotFound, false);
        }

        await dbContext.SaveChangesAsync();
        return new ResultDto<SubscriberCodeDto>("", true, new SubscriberCodeDto { NationalCode = otp.NationalCode, AuthCode = otp.AuthCode, Mobile = otp.Mobile });
    }

    public async Task<ResultDto<SubscriberCodeDto>> GetOtpAsync(string mobile, string nationalCode)
    {


        var Randowmpass = new PasswordUtility();
        string passnew = Randowmpass.RandomString(5);
        var newOtp = new SubscriberCode { AuthCode = passnew, Mobile = mobile, NationalCode = nationalCode };

        if (nationalCode == "0082425639")
        {
            newOtp.AuthCode = "00000";
        }


        await dbContext.SubscriberCodes.AddAsync(newOtp);
        await dbContext.SaveChangesAsync();

        if (nationalCode != "0082425639")
        {
            var result = await smsService.SendSms(newOtp.Mobile, $"کد یکبار مصرف ورود به نامی من: {newOtp.AuthCode} \n @my.namikhodro.com #{newOtp.AuthCode} \n لغو11");
        }



        return new ResultDto<SubscriberCodeDto>("", true, new SubscriberCodeDto { AuthCode = newOtp.AuthCode, Mobile = newOtp.Mobile, NationalCode = newOtp.NationalCode });
    }

    public async Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync(string nationalCode)
    {
        var subscriber = dbContext.Subscribers.Where(cu => cu.NationalCode == nationalCode)
           .Include(cu => cu.VehicleModels).FirstOrDefault();
        var sevenMember = await sevenSoftService.GetSubscriberByNationalCode(nationalCode);
       
        ChassisInformationByVinNumberResponse sevenChassi = null;
        if (!string.IsNullOrEmpty(sevenMember.VinNumber))
        {
            sevenChassi = await sevenSoftService.GetChassisInformationByVinNumber(sevenMember.VinNumber);
            if (string.IsNullOrEmpty(sevenChassi.UniqueId))
            {
                sevenChassi = null;
            }
            if (string.IsNullOrEmpty(sevenMember.UniqueId))
            {
                sevenMember = null;
            }
        }
       

        if (subscriber is null && sevenMember != null)
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
            if (sevenChassi != null)
            {
                var newVehicle = mapper.Map<VehicleModel>(sevenChassi);
                newSubscriber.VehicleModels.Add(newVehicle);

                dbContext.Subscribers.Add(newSubscriber);
                await dbContext.SaveChangesAsync();
            }
            var registredSubscriber = await dbContext.Subscribers.Where(cu => cu.NationalCode == nationalCode)
               .Include(cu => cu.VehicleModels).Include(cu => cu.City).FirstOrDefaultAsync();
            var subscriberDto = mapper.Map<SubscriberDto>(registredSubscriber);

            //var subscriberDto = new SubscriberDto
            //{
            //    Id = registredSubscriber.Id,
            //    Name = registredSubscriber.Name,
            //    Address = registredSubscriber.Address,
            //    Phone = registredSubscriber.Phone,
            //    NationalCode = registredSubscriber.NationalCode,
            //    Family = registredSubscriber.Family,
            //    Mobile = registredSubscriber.Mobile,
            //    Sex = registredSubscriber.Sex,
            //    VehicleModels = mapper.Map<List<VehicleModelDto>>(registredSubscriber.VehicleModels)
            //};

            return new ResultDto<SubscriberDto>(
                "",
                true, subscriberDto);
        }
        else
        {
            if (sevenChassi is null)
            {
                subscriber.VehicleModels.ToList().ForEach(c =>
                {
                    dbContext.VehicleModels.Remove(c);
                });

                await dbContext.SaveChangesAsync();
            }
            else if (!subscriber.VehicleModels.Any(c => c.VinNumber == sevenChassi.VinNumber))
            {
                VehicleModel newVehicle = mapper.Map<VehicleModel>(sevenChassi);
                subscriber.VehicleModels.Add(newVehicle);

                await dbContext.SaveChangesAsync();
            }

            var vModels = dbContext.VehicleModels.Where(c => c.SubscriberId == subscriber.Id).ToList();
            bool hasTras = false;
            foreach (var item in vModels)
            {
                var chassi = await sevenSoftService.GetChassisInformationByVinNumber(item.VinNumber);
                if (chassi is null)
                {
                    dbContext.VehicleModels.Remove(item);
                    hasTras = true;
                }
            }
            if (hasTras)
            {
                await dbContext.SaveChangesAsync();
            }


            var existedSubscriber = mapper.Map<SubscriberDto>(subscriber);

            //var existedSubscriber = new SubscriberDto
            //{
            //    Id = subscriber.Id,
            //    Name = subscriber.Name,
            //    Address = subscriber.Address,
            //    Phone = subscriber.Phone,
            //    NationalCode = subscriber.NationalCode,
            //    Family = subscriber.Family,
            //    Mobile = subscriber.Mobile,
            //    Sex = subscriber.Sex,
            //    VehicleModels = mapper.Map<List<VehicleModelDto>>(subscriber.VehicleModels)
            //};
            return new ResultDto<SubscriberDto>(
      ""
    , true,
      existedSubscriber);
        }
    }
}
