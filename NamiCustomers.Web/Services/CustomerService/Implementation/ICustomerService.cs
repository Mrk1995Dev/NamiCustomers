using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Abstractions.Dtos.Subscribers;

namespace NamiCustomers.Web.Services.CustomerService.Implementation
{
    public interface ICustomerService
    {
        Task<ResultDto> CreateAsync(RegisterSubscriberDto customer);
		Task<ResultDto> CreateAsync(RegisterSubscriberDto customer);
		Task<ResultDto> DeleteAsync(int id);
		Task<ResultDto> DeleteAsync(int id);
		Task<List<SubscriberListDto>> GetAllAsync(string mobile);
		Task<List<SubscriberListDto>> GetAllAsync(string mobile);
		Task<List<CityDto>> GetAllCitiesAsync();
		Task<List<CityDto>> GetAllCitiesAsync();
		Task<ResultDto<SubscriberDto>> GetByIdAsync(int id);
		Task<ResultDto<SubscriberDto>> GetByIdAsync(int id);
		Task GetToken(string mobile);
		Task<ResultDto> UpdateAsync(UpdateSubscriberDto updateCustomer);
		Task<ResultDto> UpdateAsync(UpdateSubscriberDto updateCustomer);
	}
}