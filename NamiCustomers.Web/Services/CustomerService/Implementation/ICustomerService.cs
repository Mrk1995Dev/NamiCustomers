using NamiCustomers.Web.Services.CustomerService.Dto;

namespace NamiCustomers.Web.Services.CustomerService.Implementation
{
    public interface ICustomerService
    {
        Task<ResultDto> CreateAsync(AddCustomerInfoDto customer);
        Task<ResultDto> DeleteAsync(int id);
        Task<List<CustomerListDto>> GetAllAsync(string mobile);
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<ResultDto<CustomerInfoDetailsDto>> GetByIdAsync(int id);
        Task<ResultDto> UpdateAsync(UpdateCustomerInfoDto updateCustomer);
    }
}