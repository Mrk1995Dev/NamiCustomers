using NamiCustomers.MVC.Services.Subscribers.Dtos;
using System.Reflection;

namespace NamiCustomers.MVC.Services.Subscribers
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

    public record MyToken(string token);


    public class SubscriberService: ISubscriberService
    {
        private readonly HttpClient _httpClient;
        public SubscriberService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SubscriberListDto>> GetAllAsync(string mobile)
        {
            await GetToken(mobile);
            var result = await _httpClient.GetFromJsonAsync<List<SubscriberListDto>>($"subscriber/subscribers");
            return result;
        }

        private async Task GetToken(string mobile)
        { 
            var myToken = await _httpClient.GetFromJsonAsync<MyToken>($"Account/GetToken?mobile={mobile}");

            _httpClient.DefaultRequestHeaders.Add("accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {myToken.token}");
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            await GetToken("09191646456");
            var result = await _httpClient.GetFromJsonAsync<List<CityDto>>($"City/list");
            return result;
        }

        public async Task<ResultDto<SubscriberDetailsDto>> GetByIdAsync(int id)
        {
            await GetToken("09191646456");
            return await _httpClient.GetFromJsonAsync<ResultDto<SubscriberDetailsDto>>($"subscriber/info?id={id}");
        }

        public async Task<ResultDto> CreateAsync(AddSubscriberDto customer)
        {
            var respone = await _httpClient.PostAsJsonAsync($"subscriber/register", customer);
            if (respone.IsSuccessStatusCode)
            {
                return new ResultDto(
                    "مشتری جدید با موفقیت ثبت شد.",
                    true);
            }

            return new ResultDto(
                "خطا در ثبت مشتری جدید",
                false);
        }

        public async Task<ResultDto> UpdateAsync(UpdateSubscriberDto updateCustomer)
        {
            var response = await _httpClient.PutAsJsonAsync($"susbcriber/edit", updateCustomer);
            if (response.IsSuccessStatusCode)
            {
                return new ResultDto(
                    "مشتری با موفقیت ویرایش شد.",
                    true);
            }

            return new ResultDto(
                "خطا در ویرایش مشتری ",
                false);
        }

        public async Task<ResultDto> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"subscriber/remove?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return new ResultDto(
                    "مشتری با موفقیت حذف شد.",
                    true);
            }

            return new ResultDto(
                "خطا در حذف مشتری ",
                false);
        }
    }
}