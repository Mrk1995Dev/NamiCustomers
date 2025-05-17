using NamiCustomers.Infrastucture.Model;
using NamiCustomers.Application.Services.Customers.Dtos;
using System.Text;

namespace NamiCustomers.Application.Services.Customers
{
    public interface ICustomerManagmentService
    {
        Task<ResultDto<List<CustomerListDto>>> GetCustomerListAsync();
        Task<ResultDto> AddCustomerInfoAsync(AddCustomerInfoDto addCustomerInfoDto);
        Task<ResultDto> DeleteCustomerInfoAsync(int customerId);
        Task<ResultDto> UpdateCustomerInfo(UpdateCustomerInfoDto updateCustomerInfoDto);
        Task<ResultDto<CustomerInfoDetailsDto>> GetCustomerInfoDetailAsync(int customerId);
        Task<ResultDto<byte[]>> ExportCustomerInfoAsync();
        Task<List<CityDto>> GetAllCitiesAsync();
    }
    public class CustomerManagmentService(IAppDbContext context) : ICustomerManagmentService
    {
        /// <summary>
        /// Add New Customer
        /// </summary>
        /// <param name="addCustomerInfoDto"></param>
        /// <returns></returns>
        public async Task<ResultDto> AddCustomerInfoAsync(AddCustomerInfoDto addCustomerInfoDto)
        {
            if (addCustomerInfoDto == null) return new ResultDto("اطلاعات وارد شده نامعتبر می باشد.", false);
            Customer newCustomer = new Customer
            {
                Name = addCustomerInfoDto.Name,
                Address = addCustomerInfoDto.Address,
                CityId = addCustomerInfoDto.CityId,
                CoWorkers = addCustomerInfoDto.CoWorkers,
                PhoneNumber = addCustomerInfoDto.PhoneNumber,
                Fax = addCustomerInfoDto.Fax
            };

            await context.Customers.AddAsync(newCustomer);
            if (await context.SaveChangesAsync() < 1) return new ResultDto("خطا در ذخیره اطلاعات مربوطه", false);

            return new ResultDto("اطلاعات با موفقیت ذخیره شد.", true);
        }

        /// <summary>
        /// Soft Delete Customer Service
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ResultDto> DeleteCustomerInfoAsync(int customerId)
        {
            if (customerId == 0)
                return new ResultDto("شناسه وارد شده نامعتبر می باشد.", false);

            var customer = await context.Customers.FirstOrDefaultAsync(cu => cu.Id == customerId);
            if (customer is null)
                return new ResultDto("کاربر مربوطه یافت نشد.", false);

            context.Customers.Remove(customer);
            if (await context.SaveChangesAsync() < 1) return new ResultDto("خطا در حذف اطلاعات مربوطه", false);

            return new ResultDto("کاربر با موفقیت حذف شد.", true);
        }

        /// <summary>
        /// Get List Of All Customers Info
        /// </summary>
        /// <returns></returns>
        public async Task<ResultDto<List<CustomerListDto>>> GetCustomerListAsync()
        {
            var customers = context.Customers.AsQueryable();

            var data = await customers.Select(c => new CustomerListDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                CityName = c.City.Title,
                CoWorkers = c.CoWorkers,
                Fax = c.Fax,
                PhoneNumber = c.PhoneNumber
            }).ToListAsync();

            return new ResultDto<List<CustomerListDto>>(
                "",
                true,
                data);
        }

        /// <summary>
        /// Update Current Customer Info
        /// </summary>
        /// <param name="updateCustomerInfoDto"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateCustomerInfo(UpdateCustomerInfoDto updateCustomerInfoDto)
        {
            var currentCustomer = await context.Customers.Where(cu => cu.Id == updateCustomerInfoDto.Id).FirstOrDefaultAsync();
            if (currentCustomer is null)
                return new ResultDto(
                    "کاربر مربوطه یافت نشد",
                    false);

            currentCustomer.Name = updateCustomerInfoDto.Name;
            currentCustomer.Address = updateCustomerInfoDto.Address;
            currentCustomer.Fax = updateCustomerInfoDto.Fax;
            currentCustomer.PhoneNumber = updateCustomerInfoDto.PhoneNumber;
            currentCustomer.CoWorkers = updateCustomerInfoDto.CoWorkers;
            currentCustomer.CityId = updateCustomerInfoDto.CityId;

            context.Customers.Update(currentCustomer);
            if (await context.SaveChangesAsync() < 1)
                return new ResultDto(
                    "خطا در ویرایش اطلاعات کاربر",
                    false);

            return new ResultDto(
                "اطلاعات کاربر با موفقیت ویرایش شد",
                true);
        }

        /// <summary>
        /// Get Detail Of Customer Info
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ResultDto<CustomerInfoDetailsDto>> GetCustomerInfoDetailAsync(int customerId)
        {

            var data = await context.Customers.Where(cu => cu.Id == customerId)
                .Include(cu => cu.City).FirstOrDefaultAsync();

            if (data == null) return new ResultDto<CustomerInfoDetailsDto>(
                "کاربر مربوطه یافت نشد.",
                false,
                null);

            var customerInfo = new CustomerInfoDetailsDto
            {
                Id = data.Id,
                Name = data.Name,
                Address = data.Address,
                CityName = data.City.Title,
                PhoneNumber = data.PhoneNumber,
                Fax = data.Fax,
                CoWorkers = data.CoWorkers
            };

            return new ResultDto<CustomerInfoDetailsDto>(
                "",
                true,
                customerInfo);
        }

        /// <summary>
        /// Export Customer Report
        /// </summary>
        /// <returns></returns>
        public async Task<ResultDto<byte[]>> ExportCustomerInfoAsync()
        {
            var customerInfos = await context.Customers
                .Include(c => c.City)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Id\tName\tAddress\tCityName\tPhoneNumber\tFax");

            foreach (var item in customerInfos)
            {
                sb.AppendLine($"{item.Id}\t{item.Name}\t{item.Address}\t{item.City.Title}\t{item.PhoneNumber}\t{item.Fax}");
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
    }

    public class CityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
