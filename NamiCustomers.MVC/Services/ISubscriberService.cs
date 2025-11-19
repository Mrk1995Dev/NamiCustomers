
using Azure;
using IdentityModel.OidcClient;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.Utilities;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

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


    public SubscriberDto CurrentSubscriber=> GetByNationalCodeAsync().GetAwaiter().GetResult().Data;
    public SubscriberService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<List<SubscriberDto>> GetAsync()
    {

        var response = await _httpClient.GetFromJsonAsync<List<SubscriberDto>>($"subscriber/subscribers");

        return response;
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
        var response= await _httpClient.GetFromJsonAsync<ResultDto<SubscriberDto>>($"subscriber/info?id={id}");
        if (response.Succeeded)
        {
            return response;
        }
        throw new Exception(response.Message);
    }
    public async Task<ResultDto> RegisterAsync(SubscriberDto customer)
    {
        var respone = await _httpClient.PostAsJsonAsync($"subscriber/register", customer);
        if (respone.IsSuccessStatusCode)
        {
            return   ResultDto.Success(  Infrastucture.Properties.Resources.msgSave );
        }

        return   ResultDto.Failure(  Infrastucture.Properties.Resources.errSave );
    }

    public async Task<ResultDto> EditAsync(SubscriberDto updateCustomer)
    {
        var response = await _httpClient.PutAsJsonAsync($"Subscriber/Edit", updateCustomer);
        if (response.IsSuccessStatusCode)
        {
            return   ResultDto.Success(Infrastucture.Properties.Resources.msgEdited);
        }

        return   ResultDto.Failure(Infrastucture.Properties.Resources.errEdited);
    }

    public async Task<ResultDto> RemoveAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"subscriber/remove?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return   ResultDto.Success(Infrastucture.Properties.Resources.msgDeleted);
        }

        return   ResultDto.Failure(Infrastucture.Properties.Resources.errDelete);
    }

    public async Task<ResultDto<SubscriberDto>> GetByNationalCodeAsync()
    {
        //var jsonSubscriber = httpContextAccessor.GetClaimValue(MyClaims.Subscriber);//moradi
        //if (!string.IsNullOrEmpty(jsonSubscriber))
        //{
        //    var subscriberDto=JsonConvert.DeserializeObject<SubscriberDto>(jsonSubscriber);   
        //    return ResultDto.Success<SubscriberDto>(subscriberDto);
        //}

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
        return ResultDto.Failure<SubscriberDto>(response.ReasonPhrase);
        // Log or handle the error response
        var errorContent = await response.Content.ReadAsStringAsync();
        // _logger.LogError($"API Error: {response.StatusCode} - {errorContent}");

        // You might want to return null or throw a custom exception
        return new ResultDto<SubscriberDto>(Infrastucture.Properties.Resources.errNotFound, false);







    }
}
