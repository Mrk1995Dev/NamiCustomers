using NamiCustomers.Abstractions.Dtos;

using NamiCustomers.Infrastucture.Utilities;
using System.Reflection;
using System.Security.Claims;

namespace NamiCustomers.MVC.Services
{
    public interface ISubscriberService
    {
        Task<ResultDto> CreateAsync(AddSubscriberDto customer);
        Task<ResultDto> DeleteAsync(int id);
        Task<List<SubscriberListDto>> GetAllAsync();
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<ResultDto<SubscriberDetailsDto>> GetByIdAsync(int id);
        Task<ResultDto<SubscriberDetailsDto>> GetByNationalCodeAsync();
        Task<ResultDto> UpdateAsync(UpdateSubscriberDto updateCustomer);
    }

    public record MyToken(string token);


    public class SubscriberService: ISubscriberService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SubscriberService(HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SubscriberListDto>> GetAllAsync()
        {
            await GetToken();
            var result = await _httpClient.GetFromJsonAsync<List<SubscriberListDto>>($"subscriber/subscribers");
            return result;
        }

        private async Task GetToken()
        {
            var mobile = httpContextAccessor.GetClaimValue(MyClaims.Mobile);
            var myToken = await _httpClient.GetFromJsonAsync<MyToken>($"Account/GetToken?mobile={mobile}");

            _httpClient.DefaultRequestHeaders.Add("accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {myToken.token}");
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            await GetToken();
            var result = await _httpClient.GetFromJsonAsync<List<CityDto>>($"City/list");
            return result;
        }

        public async Task<ResultDto<SubscriberDetailsDto>> GetByIdAsync(int id)
        {
            await GetToken();
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

        public async  Task<ResultDto<SubscriberDetailsDto>> GetByNationalCodeAsync()
        {
            
            var nationalCode = httpContextAccessor.GetClaimValue(MyClaims.NationalCode);
            await GetToken();
            return await _httpClient.GetFromJsonAsync<ResultDto<SubscriberDetailsDto>>($"subscriber/infobynationalcode?nationalcode={nationalCode}");
        }
    }
}