using NamiCustomers.Application.Services.Facades;
using NamiCustomers.Application.Services.SevenSoftServices.Dtos;
using NamiCustomers.Infrastucture.Properties;
using System.Text.Json;

namespace NamiCustomers.Application.Services.SevenSoftServices;

public interface ISevenSoftService
{
    /// <summary>
    /// بر قرار بودن ارتباط شاسی با کد ملی
    /// </summary>
    /// <param name="vinNumber"></param>
    /// <returns></returns>
    Task<ChassisInformationByVinNumberResponse> GetChassisInformationByVinNumber(string vinNumber);

    /// <summary>
    /// دریافت اطلاعات شاسی
    /// </summary>
    /// <param name="chassisVinNumber"></param>
    /// <param name="nationalCodeOrEconomicCode"></param>
    /// <param name="mobile"></param>
    /// <returns></returns>
    Task<string> GetRelationCustomerInfoByVinNumber(string chassisVinNumber, string nationalCodeOrEconomicCode, string mobile);

    /// <summary>
    /// دریافت اطلاعات کد ملی
    /// </summary>
    /// <param name="nationalCode"></param>
    /// <returns></returns>
    Task<SevenSubscriberResponse> GetSubscriberByNationalCode(string nationalCode);

}

public class SevenSoftService(ISettingsFacadeService settingsFacadeService) : ISevenSoftService
{

    public async Task<SevenSubscriberResponse> GetSubscriberByNationalCode(string nationalCode)
    {
        return await GetData<SevenSubscriberResponse>(Resource7Soft.GetSubscriberByNationalCode, nationalCode);
    }

    public async Task<ChassisInformationByVinNumberResponse> GetChassisInformationByVinNumber(string vinNumber)
    {
        return await GetData<ChassisInformationByVinNumberResponse>(Resource7Soft.GetChassisInformationByVinNumber, vinNumber);
    }
    public async Task<string> GetRelationCustomerInfoByVinNumber(string chassisVinNumber, string nationalCodeOrEconomicCode, string mobile)
    {
        string qStr = $"?ChassisVinNumber={chassisVinNumber}&NationalCodeOrEconomicCode={nationalCodeOrEconomicCode}&Mobile={mobile}";
        return await GetData<string>(Resource7Soft.RelationCustomerInfoByVinNumber, qStr);
    }

    #region Privates
    private async Task<T> GetData<T>(string apiAddress, dynamic queryString)
    {
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{settingsFacadeService.SevenSoftSetting.BaseUrl}{apiAddress}{queryString}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        return await Task.FromResult(JsonSerializer.Deserialize<T>(content));
    }

    private async Task<T> PostData<T>(string apiAddress, dynamic queryModel)
    {
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"{settingsFacadeService.SevenSoftSetting.BaseUrl}{apiAddress}");
        request.Headers.Add("Accept", "application/json");
        var content = new StringContent(JsonSerializer.Serialize(queryModel), null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
    }


    #endregion



}




 
