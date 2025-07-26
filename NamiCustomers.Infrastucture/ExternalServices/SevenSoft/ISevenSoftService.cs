using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;
using System.Text.Json;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft;

public interface ISevenSoftService
{
    /// <summary>
    /// فراخوان ها
    /// </summary>
    /// <param name="chassisVinNumber"></param>
    /// <param name="nationalCodeOrEconomicCode"></param>
    /// <param name="mobile"></param>
    /// <returns></returns>
    Task<string> GetSpecificCases(string chassisVinNumber,string nationalCodeOrEconomicCode,string mobile);

    /// <summary>
    /// لیست نمایندگی ها
    /// </summary>
    /// <returns></returns>
    Task<DealerResponse[]> GetDealers();
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
    /// <summary>
    /// استعلام گارانتی
    /// </summary>
    /// <param name="vinNumber"></param>
    /// <returns></returns>
    Task<ActiveMainChassisGuaranteeResponse> GetActiveMainChassisGuarantee(string vinNumber);

}

public class SevenSoftService : ISevenSoftService
{
    public async Task<DealerResponse[]> GetDealers()
    {
        return await GetData<DealerResponse[]>(Resource7Soft.GetDealers,"");
    }
    public async Task<ActiveMainChassisGuaranteeResponse> GetActiveMainChassisGuarantee(string vinNumber)
    {
        return await GetData<ActiveMainChassisGuaranteeResponse>(Resource7Soft.GetActiveMainChassisGuarantee, vinNumber);
    }
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

    public async Task<string> GetSpecificCases(string chassisVinNumber, string nationalCodeOrEconomicCode, string mobile)
    {
        string qStr = $"?ChassisVinNumber={chassisVinNumber}&NationalCodeOrEconomicCode={nationalCodeOrEconomicCode}&Mobile={mobile}";
        return await GetData<string>(Resource7Soft.GetSpecificCases, qStr);
    }

    #region Privates
    private async Task<T> GetData<T>(string apiAddress, dynamic queryString)
    {
        string baseUrl = Infrastucture.Properties.Resource7Soft.BaseUrl;
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{apiAddress}{queryString}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        return await Task.FromResult(JsonSerializer.Deserialize<T>(content));
    }

    private async Task<T> PostData<T>(string apiAddress, dynamic queryModel)
    {
        string baseUrl = Infrastucture.Properties.Resource7Soft.BaseUrl;
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/{apiAddress}");
        request.Headers.Add("Accept", "application/json");
        var content = new StringContent(JsonSerializer.Serialize(queryModel), null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
    }
    #endregion



}




 
