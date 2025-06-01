using NamiCustomers.MVC.Services.Subscribers.Dtos;

namespace NamiCustomers.MVC.Services.Subscribers.Implementation
{
    public interface ISubscriberService
    {
        Task<ResultDto> CreateAsync(AddSubscriberDto customer);
        Task<ResultDto> DeleteAsync(int id);
        Task<List<SubscriberListDto>> GetAllAsync(string mobile);
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<ResultDto<SubscriberDetailsDto>> GetByIdAsync(int id);
        Task<ResultDto> UpdateAsync(UpdateSubscriberDto updateCustomer);
    }
}