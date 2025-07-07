using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.CustomerService.Implementation
{
    public record MyToken(string token);


    public class CustomerService: ICustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SubscriberDto>> GetAllAsync(string mobile)
        {
            //https://localhost:7061/v1/Customer/customerList
            //https://localhost:7061/customer/customerList

            await GetToken(mobile);
            var result = await _httpClient.GetFromJsonAsync<List<SubscriberDto>>($"/api/subscriber/customerList");
            return result;
        }

        private async Task GetToken(string mobile)
        {
            var myToken = await _httpClient.GetFromJsonAsync<MyToken>($"/api/Account/GetToken?mobile={mobile}");

            _httpClient.DefaultRequestHeaders.Add("accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {myToken.token}");
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            await GetToken("09191646456");
            var result = await _httpClient.GetFromJsonAsync<List<CityDto>>($"/api/City/list");
            return result;
        }

        public async Task<ResultDto<SubscriberDto>> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ResultDto<SubscriberDto>>($"/api/customer/info?id={id}");
        }

        public async Task<ResultDto> CreateAsync(SubscriberDto customer)
        {
            //https://localhost:7061/api/v1/Customer/addCustomer
            var respone = await _httpClient.PostAsJsonAsync($"/api/customer/addCustomer", customer);
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

        public async Task<ResultDto> UpdateAsync(SubscriberDto updateCustomer)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/customer/edit", updateCustomer);
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
            var response = await _httpClient.DeleteAsync($"/api/customer/remove?id={id}");
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