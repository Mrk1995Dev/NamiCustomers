using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;
using NamiCustomers.Infrastucture.Utilities;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft;

public interface ISevenSoftService
{
    /// <summary>
    /// لیست شعب
    /// </summary>
    /// <param name="dealerId"></param>
    /// <returns></returns>
    Task<GetBranchesByDealerResponse[]> GetBranchesByDealer(Guid dealerId);

    /// <summary>
    /// سوابق تعمیراتی
    /// </summary>
    /// <param name="chassisVinNumber"></param>
    /// <returns></returns>
    Task<GetReceptionsInformationByVinNumberResponse[]> GetReceptionsInformationByVinNumber(string chassisVinNumber);
    /// <summary>
    /// فراخوان ها
    /// </summary>
    /// <param name="chassisVinNumber"></param>
    /// <param name="nationalCodeOrEconomicCode"></param>
    /// <param name="mobile"></param>
    /// <returns></returns>
    Task<string[]> GetSpecificCases(string chassisVinNumber, string nationalCodeOrEconomicCode, string mobile);

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
    private string _baseUrl = Infrastucture.Properties.Resource7Soft.BaseUrl;
    public async Task<GetBranchesByDealerResponse[]> GetBranchesByDealer(Guid dealerId)
    {
        return await RestUtility.GetData<GetBranchesByDealerResponse[]>(_baseUrl, Resource7Soft.GetBranchesByDealer, dealerId);
    }
    public async Task<GetReceptionsInformationByVinNumberResponse[]> GetReceptionsInformationByVinNumber(string chassisVinNumber)
    {
        return await RestUtility.GetData<GetReceptionsInformationByVinNumberResponse[]>(_baseUrl, Resource7Soft.GetReceptionsInformationByVinNumber, chassisVinNumber);
    }
    public async Task<DealerResponse[]> GetDealers()
    {
        return await RestUtility.GetData<DealerResponse[]>(_baseUrl, Resource7Soft.GetDealers, "");
    }
    public async Task<ActiveMainChassisGuaranteeResponse> GetActiveMainChassisGuarantee(string vinNumber)
    {
        return await RestUtility.GetData<ActiveMainChassisGuaranteeResponse>(_baseUrl, Resource7Soft.GetActiveMainChassisGuarantee, vinNumber);
    }
    public async Task<SevenSubscriberResponse> GetSubscriberByNationalCode(string nationalCode)
    {
        return await RestUtility.GetData<SevenSubscriberResponse>(_baseUrl, Resource7Soft.GetSubscriberByNationalCode, nationalCode);
    }

    public async Task<ChassisInformationByVinNumberResponse> GetChassisInformationByVinNumber(string vinNumber)
    {
        return await RestUtility.GetData<ChassisInformationByVinNumberResponse>(_baseUrl, Resource7Soft.GetChassisInformationByVinNumber, vinNumber);
    }
    public async Task<string> GetRelationCustomerInfoByVinNumber(string chassisVinNumber, string nationalCodeOrEconomicCode, string mobile)
    {
        string qStr = $"?ChassisVinNumber={chassisVinNumber}&NationalCodeOrEconomicCode={nationalCodeOrEconomicCode}&Mobile={mobile}";
        return await RestUtility.GetData<string>(_baseUrl, Resource7Soft.RelationCustomerInfoByVinNumber, qStr);
    }

    public async Task<string[]> GetSpecificCases(string chassisVinNumber, string nationalCodeOrEconomicCode, string mobile)
    {
        string qStr = $"?ChassisVinNumber={chassisVinNumber}&NationalCodeOrEconomicCode={nationalCodeOrEconomicCode}&Mobile={mobile}";
        return await RestUtility.GetData<string[]>(_baseUrl, Resource7Soft.GetSpecificCases, qStr);
    }





}





