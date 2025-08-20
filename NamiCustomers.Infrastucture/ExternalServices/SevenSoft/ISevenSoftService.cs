using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;
using NamiCustomers.Infrastucture.Utilities;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    /// <summary>
    /// دریافت اطلاعات قطعات پذیرش براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<ResultDto<GetReceptionsPartsInformationByReceptionCodeResponse[]>> GetReceptionsPartsInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode);
    /// <summary>
    /// دریافت اطلاعات خدمات داخل تعمیرگاه پذیرش براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<ResultDto<GetReceptionsInServicesInformationByReceptionCodeResponse[]>> GetReceptionsInServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode);
    /// <summary>
    /// دریافت اطلاعات خدمات خارج از تعمیرگاه پذیرش ها براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<ResultDto<GetReceptionsOutServicesInformationByReceptionCodeResponse[]>> GetReceptionsOutServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode);
    /// <summary>
    /// دریافت اطلاعات اظهارات مشتری براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<ResultDto<GetReceptionCustomerStatementInformationByReceptionCodeResponse[]>> GetReceptionCustomerStatementInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode);
    /// <summary>
    /// فاکتور پذیرش
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <returns></returns>
    Task<ResultDto<GetReceptionsInformationByReceptionIDResponse>> GetReceptionsInformationByReceptionID(string ReceptionCode);
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

    public async Task<ResultDto<GetReceptionsPartsInformationByReceptionCodeResponse[]>> GetReceptionsPartsInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data= await RestUtility.GetData<GetReceptionsPartsInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionsPartsInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");
            return new ResultDto<GetReceptionsPartsInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {

            return new ResultDto<GetReceptionsPartsInformationByReceptionCodeResponse[]>(ex.Message, false);
        }
     
    }

    public async Task<ResultDto<GetReceptionsInServicesInformationByReceptionCodeResponse[]>> GetReceptionsInServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
           var data= await RestUtility.GetData<GetReceptionsInServicesInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionsInServicesInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");

            return new ResultDto<GetReceptionsInServicesInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {

            return new ResultDto<GetReceptionsInServicesInformationByReceptionCodeResponse[]>(ex.Message, false);
        }
      
    }

    public async Task<ResultDto<GetReceptionsOutServicesInformationByReceptionCodeResponse[]>> GetReceptionsOutServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<GetReceptionsOutServicesInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionsOutServicesInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");

            return new ResultDto<GetReceptionsOutServicesInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<GetReceptionsOutServicesInformationByReceptionCodeResponse[]>(ex.Message, false);
        }
    }

    public async Task<ResultDto<GetReceptionCustomerStatementInformationByReceptionCodeResponse[]>> GetReceptionCustomerStatementInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data= await RestUtility.GetData<GetReceptionCustomerStatementInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionCustomerStatementInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");
            return new ResultDto<GetReceptionCustomerStatementInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<GetReceptionCustomerStatementInformationByReceptionCodeResponse[]>(ex.Message, false);
        }
       
    }


    public async Task<ResultDto<GetReceptionsInformationByReceptionIDResponse>> GetReceptionsInformationByReceptionID(string ReceptionCode)
    {
        try
        {
            var data = await RestUtility.PostData<GetReceptionsInformationByReceptionIDResponse>(_baseUrl, Resource7Soft.getReceptionsInformationByReceptionID, ReceptionCode);
            return new ResultDto<GetReceptionsInformationByReceptionIDResponse>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<GetReceptionsInformationByReceptionIDResponse>(ex.Message, false);
        }
    }
}





