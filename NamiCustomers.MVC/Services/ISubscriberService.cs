
using NamiCustomers.Infrastucture.Utilities;

namespace NamiCustomers.MVC.Services;

public interface ISubscriberService
{
    public SubscriberDto CurrentSubscriber { get; }
    Task<ResultDto> RegisterAsync(SubscriberDto customer);

    Task<ResultDto> RemoveAsync(int id);
    Task<List<SubscriberDto>> GetAsync();
    Task<List<CityDto>> GetCitiesAsync();
    Task<ResultDto<SubscriberDto>> GetAsync(int id);
    Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync();
    Task<ResultDto> EditAsync(SubscriberDto updateCustomer);
}
public class SubscriberService : ISubscriberService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor httpContextAccessor;

    public SubscriberDto CurrentSubscriber => GetByNationalCodeAsync().GetAwaiter().GetResult().Data;

    public SubscriberService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<List<SubscriberDto>> GetAsync()
    {

        var result = await _httpClient.GetFromJsonAsync<List<SubscriberDto>>($"subscriber/subscribers");
        return result;
    }

    private void GetToken()
    {
        var mobile = httpContextAccessor.GetClaimValue(MyClaims.Mobile);
        var myToken = _httpClient.GetFromJsonAsync<MyToken>($"Account/GetToken?mobile={mobile}").Result;

        _httpClient.DefaultRequestHeaders.Add("accept", "*/*");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {myToken.token}");
    }

    public async Task<List<CityDto>> GetCitiesAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<CityDto>>($"City/list");
        return result;
    }

    public async Task<ResultDto<SubscriberDto>> GetAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ResultDto<SubscriberDto>>($"subscriber/info?id={id}");
    }
    public async Task<ResultDto> RegisterAsync(SubscriberDto customer)
    {
        var respone = await _httpClient.PostAsJsonAsync($"subscriber/register", customer);
        if (respone.IsSuccessStatusCode)
        {
            return new ResultDto(
                 Infrastucture.Properties.Resources.msgSave,
                true);
        }

        return new ResultDto(
           Infrastucture.Properties.Resources.errSave,
            false);
    }

    public async Task<ResultDto> EditAsync(SubscriberDto updateCustomer)
    {
        var response = await _httpClient.PutAsJsonAsync($"Subscriber/Edit", updateCustomer);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgEdited, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errEdited, false);
    }

    public async Task<ResultDto> RemoveAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"subscriber/remove?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgDeleted, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errDelete, false);
    }

    public async Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync()
    {

        var nationalCode = httpContextAccessor.GetClaimValue(MyClaims.NationalCode);

        var response = await _httpClient.GetAsync($"subscriber/InfoByNationalCode?nationalcode={nationalCode}");

        if (response.IsSuccessStatusCode)
        {
            var result = await _httpClient.GetFromJsonAsync<ResultDto<SubscriberDto>>($"subscriber/InfoByNationalCode?nationalcode={nationalCode}");
            if (result?.Data != null)
            {
                return new ResultDto<SubscriberDto>(Infrastucture.Properties.Resources.msgFound, true,
               ((ResultDto<SubscriberDto>)result).Data);
            }
        }

        // Log or handle the error response
        var errorContent = await response.Content.ReadAsStringAsync();
        // _logger.LogError($"API Error: {response.StatusCode} - {errorContent}");

        // You might want to return null or throw a custom exception
        return new ResultDto<SubscriberDto>(Infrastucture.Properties.Resources.errNotFound, false);







    }
}
