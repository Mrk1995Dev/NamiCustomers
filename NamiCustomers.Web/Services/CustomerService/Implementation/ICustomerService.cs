using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Abstractions.Dtos.Subscribers;

namespace NamiCustomers.Web.Services.CustomerService.Implementation
{
    public interface ICustomerService
    {
        Task<ResultDto> CreateAsync(AddSubscriberDto customer);
        Task<ResultDto> DeleteAsync(int id);
        Task<List<SubscriberListDto>> GetAllAsync(string mobile);
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<ResultDto<SubscriberDetailsDto>> GetByIdAsync(int id);
        Task<ResultDto> UpdateAsync(UpdateSubscriberDto updateCustomer);
    }
}