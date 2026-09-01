using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.Accounts;
using NamiCustomers.Application.Services.Vehicles;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Domain.Entities.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Utilities;
using Newtonsoft.Json;
using System.Collections.Immutable;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace NamiCustomers.Application.Services.Subscribers;

public interface ISubscriberService
{
    Task<ResultDto<List<SubscriberDto>>> GetAllAsync();
    Task<ResultDto<SubscriberDto>> RegisterAsync(SubscriberDto subscriberDto);
    Task<ResultDto<Subscriber>> DeleteAsync(int id);
    Task<ResultDto<Subscriber>> EditAsync(SubscriberDto updateCustomerInfoDto);
    Task<ResultDto<SubscriberDto>> GetAsync(int id);
    Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync(string nationalCode);
    Task<ResultDto<SubscriberDto>> GetAsync(string mobile);
    Task<ResultDto<byte[]>> ExportAsync();
    Task<ResultDto<List<CityDto>>> GetCitiesAsync();
    Task<ResultDto<SubscriberCodeDto>> SendOtpAsync(string mobile);
    Task<ResultDto<SubscriberCodeDto>> GetOtpAsync(string nationalCode, string mobile);







}
public class SubscriberService(IMapper mapper, ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IAppDbContext dbContext, ISmsService smsService, ISevenSoftService sevenSoftService, IVehicleService vehicleService, UserManager<ApplicationUser> userManager) : ISubscriberService
{
    public async Task<ResultDto<SubscriberDto>> RegisterAsync(SubscriberDto subscriber)
    {
        if (subscriber == null)
            return ResultDto.Failure<SubscriberDto>(Infrastucture.Properties.Resources.errInputInValid);
        if (!subscriber.Mobile.IsValidIranianMobileNumber())
        {
            return ResultDto.Failure<SubscriberDto>(Infrastucture.Properties.Resources.errInvalidMobile);
        }
        if (subscriber.SubscriberType == (int)SubscriberType.Haghighi)
        {
            var isValid = subscriber.NationalCode.IsValid();
            if (isValid)
            {
                ResultDto.Failure<SubscriberDto>(Infrastucture.Properties.Resources.errInvalidNationalCode);
            }
        }
        else
        {
            var isValid = subscriber.NationalCode.IsValidNationalId();
            if (isValid)
            {
                ResultDto.Failure<SubscriberDto>(Infrastucture.Properties.Resources.errInvalidNationalCode);
            }
        }
        Subscriber newCustomer = new Subscriber
        {
            Name = subscriber.Name,
            Family = subscriber.Family,
            Mobile = subscriber.Mobile,
            NationalCode = subscriber.NationalCode,
            Sex = subscriber.Sex,
            BrithDate = subscriber.BrithDate,
            BrithDatePersian = subscriber.BrithDatePersian,
            SubscriberType = subscriber.SubscriberType,
        };

        await dbContext.Subscribers.AddAsync(newCustomer);
        if (await dbContext.SaveChangesAsync() < 1)
            return ResultDto.Failure<SubscriberDto>(Infrastucture.Properties.Resources.errSave);
        var dto = mapper.Map<SubscriberDto>(newCustomer);

        return ResultDto.Success<SubscriberDto>(dto);
    }

    public async Task<ResultDto<Subscriber>> DeleteAsync(int customerId)//todo moradi replace by dto
    {
        if (customerId == 0)
            return ResultDto.Failure<Subscriber>(Infrastucture.Properties.Resources.errInputInValid);

        var customer = await dbContext.Subscribers.FirstOrDefaultAsync(cu => cu.Id == customerId);
        if (customer is null)
            return ResultDto.Failure<Subscriber>(Infrastucture.Properties.Resources.errSubscriberNotFound);

        dbContext.Subscribers.Remove(customer);
        if (await dbContext.SaveChangesAsync() < 1) return ResultDto.Failure<Subscriber>(Infrastucture.Properties.Resources.errDelete);

        return ResultDto.Success<Subscriber>(customer);
    }

    public async Task<ResultDto<List<SubscriberDto>>> GetAllAsync()
    {
        var customers = dbContext.Subscribers.AsQueryable();

        var result = await customers.Select(data => new SubscriberDto
        {
            Id = data.Id,
            Name = data.Name,
            Family = data.Family,
            NationalCode = data.NationalCode,
            Mobile = data.Mobile,
            Sex = data.Sex,
            BrithDate = data.BrithDate,
            BrithDatePersian = data.BrithDatePersian,
            SubscriberType = data.SubscriberType,
        }).ToListAsync();

        return ResultDto.Success<List<SubscriberDto>>(result);
    }

    public async Task<ResultDto<Subscriber>> EditAsync(SubscriberDto subscriberDto)
    {
        try
        {
            var subscriber = await dbContext.Subscribers.Where(cu => cu.Id == subscriberDto.Id).FirstOrDefaultAsync();

            if (subscriber is null)
                return ResultDto.Failure<Subscriber>(Infrastucture.Properties.Resources.errNotFound);

            subscriber.Name = subscriberDto.Name;
            subscriber.Family = subscriberDto.Family;
            //subscriber.Mobile = subscriberDto.Mobile;//موبایل نباید عوض شود
            //subscriber.NationalCode = subscriberDto.NationalCode;نباید کد ملی را عوض کند
            subscriber.Sex = subscriberDto.Sex;
            subscriber.BrithDatePersian = subscriberDto.BrithDatePersian;
            subscriber.BrithDate = subscriberDto.BrithDate;
            subscriber.SubscriberType = subscriberDto.SubscriberType;
            subscriber.Address = subscriberDto.Address;
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
                return ResultDto.Failure<Subscriber>(Infrastucture.Properties.Resources.errEdited);
            return ResultDto.Success<Subscriber>(subscriber);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<Subscriber>(ex.Message);
        }
    }


    public async Task<ResultDto<SubscriberDto>> GetAsync(int id)
    {

        var data = await dbContext.Subscribers.Where(cu => cu.Id == id)
            .Include(cu => cu.City).Include(cu => cu.VehicleModels).FirstOrDefaultAsync();

        if (data == null) return   ResultDto.Failure<SubscriberDto>(
             Infrastucture.Properties.Resources.errNotFound);

        var subscriberDto = new SubscriberDto
        {
            Id = data.Id,
            Name = data.Name,
            Family = data.Family,
            NationalCode = data.NationalCode,
            Mobile = data.Mobile,
            Sex = data.Sex,
            BrithDate = data.BrithDate,
            Phone = data.Phone,
            Address = data.Address,
            CityId = data.CityId,
            CityName = data.City?.Title,
            BrithDatePersian = data.BrithDatePersian,
            SubscriberType = data.SubscriberType,
            VehicleModels = mapper.Map<List<VehicleModelDto>>(data.VehicleModels)
        };
        return ResultDto.Success<SubscriberDto>(subscriberDto);
    }


    public async Task<ResultDto<SubscriberDto>> GetAsync(string mobile)
    {

        var data = await dbContext.Subscribers.Where(cu => cu.Mobile == mobile)
            .Include(cu => cu.City).Include(cu => cu.VehicleModels).FirstOrDefaultAsync();

        if (data == null) return ResultDto.Failure<SubscriberDto>(
           Infrastucture.Properties.Resources.errNotFound);

        var customerInfo = new SubscriberDto
        {
            Id = data.Id,
            Name = data.Name,
            Family = data.Family,
            NationalCode = data.NationalCode,
            Mobile = data.Mobile,
            Sex = data.Sex,
            BrithDate = data.BrithDate,
            BrithDatePersian = data.BrithDatePersian,
            SubscriberType = data.SubscriberType,
            VehicleModels = mapper.Map<List<VehicleModelDto>>(data.VehicleModels)
        };
        return ResultDto.Success<SubscriberDto>(customerInfo);
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

        return   ResultDto.Success
            <byte[]>( bytes);
    }

    public async Task<ResultDto<List<CityDto>>> GetCitiesAsync()
    {
        var cities = await dbContext.Cities.ToListAsync();

        var data = cities.Select(c => new CityDto
        {
            Id = c.Id,
            Title = c.Title
        }).ToList();

        return ResultDto.Success<List<CityDto>>(data);
    }

    public async Task<ResultDto<SubscriberCodeDto>> SendOtpAsync(string authCode)
    {
        var otp = await dbContext.SubscriberCodes.FirstOrDefaultAsync(c => !c.Used && c.AuthCode == authCode);
        if (otp is null)
        {
            return   ResultDto.Failure<SubscriberCodeDto>(Infrastucture.Properties.Resources.errNotFound);
        }
        otp.Used = true;
        await dbContext.SaveChangesAsync();
        return ResultDto.Success<SubscriberCodeDto>(new SubscriberCodeDto { NationalCode = otp.NationalCode, AuthCode = otp.AuthCode, Mobile = otp.Mobile });
    }

    public async Task<ResultDto<SubscriberCodeDto>> GetOtpAsync(string nationalCode, string mobile)
    {
        if (string.IsNullOrWhiteSpace(nationalCode) || string.IsNullOrWhiteSpace(mobile))
            return ResultDto.Failure<SubscriberCodeDto>(Infrastucture.Properties.Resources.errInputInValid);

        var subscriber = await dbContext.Subscribers
            .FirstOrDefaultAsync(s => s.NationalCode == nationalCode);

        if (subscriber is null)
        {
            var importResult = await ImportSubscriberFromSevenSoftAsync(nationalCode, mobile);
            if (!importResult.Succeeded)
                return ResultDto.Failure<SubscriberCodeDto>(importResult.Message);
        }

        var Randowmpass = new PasswordUtility();
        string passnew = Randowmpass.RandomString(5);
        var newOtp = new SubscriberCode { AuthCode = passnew, Mobile = mobile, NationalCode = nationalCode, Used = false };

        await dbContext.SubscriberCodes.AddAsync(newOtp);
        await dbContext.SaveChangesAsync();

        await smsService.SendSms(newOtp.Mobile, $"کد یکبار مصرف ورود به نامی من: {newOtp.AuthCode} \n @my.namikhodro.com #{newOtp.AuthCode} \n لغو11");

        return ResultDto.Success(new SubscriberCodeDto { AuthCode = newOtp.AuthCode, Mobile = newOtp.Mobile, NationalCode = newOtp.NationalCode });
    }

    private async Task<ResultDto> ImportSubscriberFromSevenSoftAsync(string nationalCode, string mobile)
    {
        SevenSubscriberResponse sevenMember;
        try
        {
            sevenMember = await sevenSoftService.GetSubscriberByNationalCode(nationalCode);
        }
        catch
        {
            return ResultDto.Failure("در حال حاضر امکان استعلام از سامانه وجود ندارد. لطفا بعدا تلاش کنید.");
        }

        if (!HasSevenSoftProfile(sevenMember))
            return ResultDto.Failure("امکان ثبت‌نام وجود ندارد. اطلاعات شما در سامانه فروش یافت نشد.");

        var newSubscriber = MapSevenSoftSubscriber(sevenMember, nationalCode, mobile);
        await dbContext.Subscribers.AddAsync(newSubscriber);
        await dbContext.SaveChangesAsync();

        try
        {
            await SaveVehiclesFromSevenSoftAsync(newSubscriber, sevenMember, nationalCode);
        }
        catch
        {
            // مشترک ذخیره شده؛ ارسال OTP نباید به‌خاطر خودرو متوقف شود
        }

        return ResultDto.Success("مشترک با موفقیت از سامانه فروش ثبت شد.");
    }

    private static bool HasSevenSoftProfile(SevenSubscriberResponse? member)
    {
        if (member is null)
            return false;

        return !string.IsNullOrWhiteSpace(member.UniqueId)
            || !string.IsNullOrWhiteSpace(member.NationalCode)
            || !string.IsNullOrWhiteSpace(member.Name)
            || !string.IsNullOrWhiteSpace(member.FirstName);
    }

    private static Subscriber MapSevenSoftSubscriber(SevenSubscriberResponse sevenMember, string nationalCode, string mobile)
    {
        var name = FirstNonEmpty(sevenMember.Name, sevenMember.FirstName, "نامشخص");
        var family = FirstNonEmpty(sevenMember.LastName, "نامشخص");

        return new Subscriber
        {
            Name = Truncate(name, 50),
            Family = Truncate(family, 50),
            Address = Truncate(sevenMember.Address, 250),
            BrithDate = sevenMember.BirthDate == default ? null : sevenMember.BirthDate,
            BrithDatePersian = sevenMember.StrBirthDate,
            FathersName = Truncate(sevenMember.FatherName, 50),
            IdNumber = Truncate(sevenMember.IdNumber, 10),
            EconomicCode = Truncate(sevenMember.EconomicCode?.ToString(), 17),
            Phone = Truncate(sevenMember.Tel, 15),
            NationalCode = FirstNonEmpty(sevenMember.NationalCode, nationalCode),
            Mobile = FirstNonEmpty(mobile, sevenMember.Mobile),
            Sex = MapGender(sevenMember.Gender),
            SubscriberType = sevenMember.IsLegalSubscriber ? (int)SubscriberType.Hogooghi : (int)SubscriberType.Haghighi
        };
    }

    private async Task SaveVehiclesFromSevenSoftAsync(Subscriber subscriber, SevenSubscriberResponse sevenMember, string nationalCode)
    {
        var existingVins = (await dbContext.VehicleModels
                .Where(v => !string.IsNullOrEmpty(v.VinNumber))
                .Select(v => v.VinNumber)
                .ToListAsync())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var vehicles = new List<VehicleModel>();

        ChassisInformationResponse[] chassiList;
        try
        {
            chassiList = await sevenSoftService.GetAllChassisInformation(nationalCode) ?? [];
        }
        catch
        {
            chassiList = [];
        }

        foreach (var item in chassiList.Where(c => !string.IsNullOrWhiteSpace(c.VinNumber)))
        {
            if (existingVins.Contains(item.VinNumber))
                continue;

            vehicles.Add(MapChassisToVehicle(item, subscriber.Id));
            existingVins.Add(item.VinNumber);
        }

        if (!string.IsNullOrWhiteSpace(sevenMember.VinNumber) && !existingVins.Contains(sevenMember.VinNumber))
        {
            vehicles.Add(MapSevenMemberVehicle(sevenMember, subscriber.Id));
        }

        if (vehicles.Count == 0)
            return;

        var hasDefault = await dbContext.VehicleModels.AnyAsync(v => v.SubscriberId == subscriber.Id && v.IsDefault);
        if (!hasDefault)
            vehicles[0].IsDefault = true;

        await dbContext.VehicleModels.AddRangeAsync(vehicles);
        await dbContext.SaveChangesAsync();
    }

    private VehicleModel MapChassisToVehicle(ChassisInformationResponse item, int subscriberId)
    {
        var vehicle = new VehicleModel
        {
            BodyColor = item.BodyColor,
            ChassisUsageTypeName = item.ChassisUsageTypeName,
            FullSystem = item.FullSystem,
            MotorNumber = item.MotorNumber,
            ProductYear = item.ProductYear,
            SelectedVehicleCommonName = item.SelectedVehicleCommonName,
            SelectedVehicleDescription = item.SelectedVehicleDescription,
            VehicleModelIdSevenSoft = item.VehicleModelId,
            VinNumber = item.VinNumber,
            IsDefault = false,
            SubscriberId = subscriberId
        };

        AttachVehicleIfNeeded(vehicle);
        return vehicle;
    }

    private VehicleModel MapSevenMemberVehicle(SevenSubscriberResponse sevenMember, int subscriberId)
    {
        var vehicle = new VehicleModel
        {
            BodyColor = sevenMember.BodyColor,
            MotorNumber = sevenMember.MotorNumber?.ToString(),
            ProductYear = sevenMember.ProductYear?.ToString(),
            SelectedVehicleCommonName = sevenMember.ChassisVehicleModelName?.ToString(),
            VinNumber = sevenMember.VinNumber,
            IsDefault = false,
            SubscriberId = subscriberId
        };

        AttachVehicleIfNeeded(vehicle);
        return vehicle;
    }

    private void AttachVehicleIfNeeded(VehicleModel vehicle)
    {
        if (!vehicle.VehicleModelIdSevenSoft.HasValue)
            return;

        var exists = dbContext.VehicleAttachments
            .Any(c => c.VehicleModelIdSevenSoft == vehicle.VehicleModelIdSevenSoft);
        if (!exists)
            vehicle.VehicleAttachment = new VehicleAttachment { VehicleModelIdSevenSoft = vehicle.VehicleModelIdSevenSoft };
    }

    private static string? MapGender(int gender)
    {
        if (Enum.IsDefined(typeof(GenderType), gender))
            return ((GenderType)gender).GetEnumDescription();

        return null;
    }

    private static string FirstNonEmpty(params string?[] values)
    {
        return values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v))?.Trim() ?? string.Empty;
    }

    private static string? Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        value = value.Trim();
        return value.Length <= maxLength ? value : value[..maxLength];
    }

    public async Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync(string nationalCode)
    {
        var subscriber = await dbContext.Subscribers
            .Include(cu => cu.VehicleModels)
            .Where(cu => cu.NationalCode == nationalCode)
            .FirstOrDefaultAsync();

        if (subscriber is null)
            return new ResultDto<SubscriberDto>("کاربر مربوطه یافت نشد.", false);
        //                  .Include(cu => cu.VehicleModels).FirstOrDefault();
        //moradi
        //var user = await userManager.Users.FirstOrDefaultAsync(c => c.NationalCode == nationalCode);
        //var token = await tokenService.GetTokenInfoAsync(user);
        //Subscriber subscriber =null;
        //if (token.HasValidToken)
        //{
        //    var jsonSubscriber = httpContextAccessor.GetClaimValue(MyClaims.Subscriber);
        //    if (!string.IsNullOrEmpty(jsonSubscriber))
        //    {
        //        var subscriberDto = JsonConvert.DeserializeObject<SubscriberDto>(jsonSubscriber);
        //        return ResultDto.Success<SubscriberDto>(subscriberDto);
        //    }
        //    else
        //    {
        //        subscriber =  dbContext.Subscribers.Where(cu => cu.NationalCode == nationalCode)
        //                  .Include(cu => cu.VehicleModels).FirstOrDefault();
        //    }
        //}
        //else
        //{
        //    subscriber = dbContext.Subscribers.Where(cu => cu.NationalCode == nationalCode)
        //                   .Include(cu => cu.VehicleModels).FirstOrDefault();
        //}

        var sevenMember = await sevenSoftService.GetSubscriberByNationalCode(nationalCode);
        var chassiList = await sevenSoftService.GetAllChassisInformation(nationalCode);

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
                Sex = ((GenderType)sevenMember.Gender).GetEnumDescription(),
            };
            dbContext.Subscribers.Add(newSubscriber);
            await dbContext.SaveChangesAsync();
            if (chassiList.Any())
            {
                foreach (var item in chassiList)
                {
                    var newVehicle = new VehicleModelDto
                    {
                        BodyColor = item.BodyColor,
                        ChassisUsageTypeName = item.ChassisUsageTypeName,
                        FullSystem = item.FullSystem,
                        MotorNumber = item.MotorNumber,
                        ProductYear = item.ProductYear,
                        SelectedVehicleCommonName = item.SelectedVehicleCommonName,
                        SelectedVehicleDescription = item.SelectedVehicleDescription,
                        VehicleModelIdSevenSoft = item.VehicleModelId,
                        VinNumber = item.VinNumber,
                        IsDefault = false,
                        SubscriberId = newSubscriber.Id,
                        VehicleModelLocalizedName = item.VehicleModelLocalizedName,
                        VehicleModelName = item.VehicleModelName,
                        Mobile = newSubscriber.Mobile,
                        NationalCode = newSubscriber.NationalCode
                    };
                    await vehicleService.RegisterAsync(newVehicle);
                }
            }
            var registredSubscriber = await dbContext.Subscribers.Where(cu => cu.NationalCode == nationalCode)
               .Include(cu => cu.VehicleModels).Include(cu => cu.City).FirstOrDefaultAsync();
            var subscriberDto = mapper.Map<SubscriberDto>(registredSubscriber);

            return ResultDto.Success<SubscriberDto>(subscriberDto);
        }
        else
        {
            if (!chassiList.Any())
            {
                subscriber.VehicleModels.ToList().ForEach(c =>
                {
                    dbContext.VehicleModels.Remove(c);
                });

                await dbContext.SaveChangesAsync();
            }
            else if (!subscriber.VehicleModels.Any(c => chassiList.Select(r => r.VinNumber).Contains(c.VinNumber)))
            {
                var newVehicleList = chassiList.Where(c => !subscriber.VehicleModels.Select(r => r.VinNumber).Contains(c.VinNumber)).ToList
                    ();
                foreach (var item in newVehicleList)
                {
                    var vehicle = new VehicleModelDto
                    {
                        BodyColor = item.BodyColor,
                        ChassisUsageTypeName = item.ChassisUsageTypeName,
                        FullSystem = item.FullSystem,
                        MotorNumber = item.MotorNumber,
                        ProductYear = item.ProductYear,
                        SelectedVehicleCommonName = item.SelectedVehicleCommonName,
                        SelectedVehicleDescription = item.SelectedVehicleDescription,
                        VehicleModelIdSevenSoft = item.VehicleModelId,
                        VinNumber = item.VinNumber,
                        IsDefault = false,
                        SubscriberId = subscriber.Id,
                        VehicleModelLocalizedName = item.VehicleModelLocalizedName,
                        VehicleModelName = item.VehicleModelName,
                        Mobile = subscriber.Mobile,
                        NationalCode = subscriber.NationalCode
                    };
                    await vehicleService.RegisterAsync(vehicle);

                }
            }

            var vModels = dbContext.VehicleModels.Where(c => c.SubscriberId == subscriber.Id).ToList();
            bool hasTrash = false;
            foreach (var item in vModels)
            {
                var chassi = await sevenSoftService.GetChassisInformationByVinNumber(item.VinNumber);
                if (chassi is null)
                {
                    dbContext.VehicleModels.Remove(item);
                    hasTrash = true;
                }
            }
            if (hasTrash)
            {
                await dbContext.SaveChangesAsync();
            }
            var existedSubscriber = mapper.Map<SubscriberDto>(subscriber);
            return ResultDto.Success<SubscriberDto>(existedSubscriber);
        }
    }
}
