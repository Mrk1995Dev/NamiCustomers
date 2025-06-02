using NamiCustomers.Application.Services.Subscribers.Dtos;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Infrastucture.Model;
using NamiCustomers.Infrastucture.Utilities;
using System.Text;
using static System.Net.WebRequestMethods;

namespace NamiCustomers.Application.Services.Subscribers
{
    public interface ISubscriberService
    {
        Task<ResultDto<List<SubscriberListDto>>> GetCustomerListAsync();
        Task<ResultDto> AddCustomerInfoAsync(AddSubscriberDto addCustomerInfoDto);
        Task<ResultDto> DeleteCustomerInfoAsync(int customerId);
        Task<ResultDto> UpdateCustomerInfo(UpdateSubscriberDto updateCustomerInfoDto);
        Task<ResultDto<SubscriberDetailsDto>> GetCustomerInfoDetailAsync(int customerId);
		Task<ResultDto<SubscriberDetailsDto>> GetCustomerInfoDetailMobileAsync(string mobile);
		Task<ResultDto<byte[]>> ExportCustomerInfoAsync();
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<ResultDto<SubscriberCodeDto>> SendOtp(string mobile);
        Task<ResultDto<SubscriberCodeDto>> GetOtp(string mobile);
    }
    public class SubscriberService(IAppDbContext context,ISmsService smsService) : ISubscriberService
    {
         
        public async Task<ResultDto> AddCustomerInfoAsync(AddSubscriberDto addCustomerInfoDto)
        {
            if (addCustomerInfoDto == null) return new ResultDto("اطلاعات وارد شده نامعتبر می باشد.", false);
            Subscriber newCustomer = new Subscriber
            {
                Name = addCustomerInfoDto.Name,
                Address = addCustomerInfoDto.Address,
                CityId = addCustomerInfoDto.CityId,
                Mobile = addCustomerInfoDto.PhoneNumber,
            };

            await context.Subscribers.AddAsync(newCustomer);
            if (await context.SaveChangesAsync() < 1) return new ResultDto("خطا در ذخیره اطلاعات مربوطه", false);

            return new ResultDto("اطلاعات با موفقیت ذخیره شد.", true);
        }
 
        public async Task<ResultDto> DeleteCustomerInfoAsync(int customerId)
        {
            if (customerId == 0)
                return new ResultDto("شناسه وارد شده نامعتبر می باشد.", false);

            var customer = await context.Subscribers.FirstOrDefaultAsync(cu => cu.Id == customerId);
            if (customer is null)
                return new ResultDto("کاربر مربوطه یافت نشد.", false);

            context.Subscribers.Remove(customer);
            if (await context.SaveChangesAsync() < 1) return new ResultDto("خطا در حذف اطلاعات مربوطه", false);

            return new ResultDto("کاربر با موفقیت حذف شد.", true);
        }
 
        public async Task<ResultDto<List<SubscriberListDto>>> GetCustomerListAsync()
        {
            var customers = context.Subscribers.AsQueryable();

            var data = await customers.Select(c => new SubscriberListDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                CityName = c.City.Title,
                PhoneNumber = c.Mobile
            }).ToListAsync();

            return new ResultDto<List<SubscriberListDto>>(
                "",
                true,
                data);
        }
 
        public async Task<ResultDto> UpdateCustomerInfo(UpdateSubscriberDto updateCustomerInfoDto)
        {
            var currentCustomer = await context.Subscribers.Where(cu => cu.Id == updateCustomerInfoDto.Id).FirstOrDefaultAsync();
            if (currentCustomer is null)
                return new ResultDto(
                    "کاربر مربوطه یافت نشد",
                    false);

            currentCustomer.Name = updateCustomerInfoDto.Name;
            currentCustomer.Address = updateCustomerInfoDto.Address;
            currentCustomer.Mobile = updateCustomerInfoDto.PhoneNumber;
            currentCustomer.CityId = updateCustomerInfoDto.CityId;

            context.Subscribers.Update(currentCustomer);
            if (await context.SaveChangesAsync() < 1)
                return new ResultDto(
                    "خطا در ویرایش اطلاعات کاربر",
                    false);

            return new ResultDto(
                "اطلاعات کاربر با موفقیت ویرایش شد",
                true);
        }

       
        public async Task<ResultDto<SubscriberDetailsDto>> GetCustomerInfoDetailAsync(int customerId)
        {

            var data = await context.Subscribers.Where(cu => cu.Id == customerId)
                .Include(cu => cu.City).FirstOrDefaultAsync();

            if (data == null) return new ResultDto<SubscriberDetailsDto>(
                "کاربر مربوطه یافت نشد.",
                false,
                null);

            var customerInfo = new SubscriberDetailsDto
            {
                Id = data.Id,
                Name = data.Name,
                Address = data.Address,
                CityName = data.City.Title,
                PhoneNumber = data.Mobile,
            };

            return new ResultDto<SubscriberDetailsDto>(
                "",
                true,
                customerInfo);
        }


		public async Task<ResultDto<SubscriberDetailsDto>> GetCustomerInfoDetailMobileAsync(string mobile)
		{

			var data = await context.Subscribers.Where(cu => cu.Mobile == mobile)
				.Include(cu => cu.City).FirstOrDefaultAsync();

			if (data == null) return new ResultDto<SubscriberDetailsDto>(
				"کاربر مربوطه یافت نشد.",
				false,
				null);

			var customerInfo = new SubscriberDetailsDto
			{
				Id = data.Id,
				Name = data.Name,
				Address = data.Address,
				CityName = data.City.Title,
				PhoneNumber = data.Mobile,
			};

			return new ResultDto<SubscriberDetailsDto>(
				"",
				true,
				customerInfo);
		}

		public async Task<ResultDto<byte[]>> ExportCustomerInfoAsync()
        {
            var customerInfos = await context.Subscribers
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

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            var cities = await context.Cities.ToListAsync();

            var data = cities.Select(c => new CityDto
            {
                Id = c.Id,
                Title = c.Title
            }).ToList();

            return data;
        }

        public async Task<ResultDto<SubscriberCodeDto>> SendOtp(string authCode)
        {
            var otp =await  context.SubscriberCodes.FirstOrDefaultAsync(c => !c.Used && c.AuthCode == authCode);
            if (otp is null)
            {
                return new ResultDto<SubscriberCodeDto>("Not found !",false,new SubscriberCodeDto());
            }
          
            otp.Used = true;
            await  context.SaveChangesAsync();
            return new ResultDto<SubscriberCodeDto>("",true,new SubscriberCodeDto { AuthCode = otp.AuthCode,Mobile=otp.Mobile });
        }

        public async Task<ResultDto<SubscriberCodeDto>> GetOtp(string mobile)
        {
            var Randowmpass = new RandomPasswordUtility();
            string passnew = Randowmpass.RandomString(5);
            var newOtp = new SubscriberCode { AuthCode = passnew,Mobile=mobile };
            await context.SubscriberCodes.AddAsync(newOtp);
            await context.SaveChangesAsync();
			await smsService.SendSms(newOtp.Mobile, $"{newOtp.AuthCode}\n لغو11");
			return new ResultDto<SubscriberCodeDto>("", true, new SubscriberCodeDto {AuthCode= newOtp.AuthCode, Mobile = newOtp.Mobile });
        }
    }

    public class CityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
