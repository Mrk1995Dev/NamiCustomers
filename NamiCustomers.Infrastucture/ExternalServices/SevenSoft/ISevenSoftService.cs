using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;
using NamiCustomers.Infrastucture.Utilities;
using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft;

public interface ISevenSoftService
{
    /// <summary>
    /// لیست شعب
    /// </summary>
    /// <param name="dealerId"></param>
    /// <returns></returns>
    Task<BranchesByDealerResponse[]> GetBranchesByDealer(Guid dealerId);
    /// <summary>
    /// سوابق تعمیراتی
    /// </summary>
    /// <param name="chassisVinNumber"></param>
    /// <returns></returns>
    Task<ReceptionsInformationByVinNumberResponse[]> GetReceptionsInformationByVinNumber(string chassisVinNumber);
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
    Task<ResultDto<ReceptionsPartsInformationByReceptionCodeResponse[]>> GetReceptionsPartsInformationByReceptionCode(string receptionCode, string nationalCodeOrEconomicCode);
    /// <summary>
    /// دریافت اطلاعات خدمات داخل تعمیرگاه پذیرش براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<ResultDto<ReceptionsInServicesInformationByReceptionCodeResponse[]>> GetReceptionsInServicesInformationByReceptionCode(string receptionCode, string nationalCodeOrEconomicCode);
    /// <summary>
    /// دریافت اطلاعات خدمات خارج از تعمیرگاه پذیرش ها براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<ResultDto<ReceptionsOutServicesInformationByReceptionCodeResponse[]>> GetReceptionsOutServicesInformationByReceptionCode(string receptionCode, string nationalCodeOrEconomicCode);
    /// <summary>
    /// دریافت اطلاعات اظهارات مشتری براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<ResultDto<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>> GetReceptionCustomerStatementInformationByReceptionCode(string receptionCode, string nationalCodeOrEconomicCode);
    /// <summary>
    /// فاکتور پذیرش
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <returns></returns>
    Task<ResultDto<ReceptionsInformationByReceptionIDResponse>> GetReceptionsInformationByReceptionID(string receptionCode);
    /// <summary>
    /// مشاهده لیست استان ها
    /// </summary>
    /// <param name="CountryId"></param>
    /// <returns></returns>
    Task<ResultDto<SubCountryResponse[]>> GetAllSubCountries(Guid countryId);
    /// <summary>
    /// مشاهده لیست شهر ها ایدی های از وب سرویس بالا میاد که شهر ها فعال فیلتر بشه
    /// </summary>
    /// <param name="SubCountryId"></param>
    /// <returns></returns>
    Task<ResultDto<CityResponse[]>> GetAllCity(Guid subCountryId);
    /// <summary>
    /// لیست نمایندگی های فعال آن شهر ای دی از وب سرویس قبلی میاد
    /// </summary>
    /// <param name="CityId"></param>
    /// <returns></returns>
    Task<ResultDto<DealerResponse[]>> GetAllDealer(Guid cityId);
    /// <summary>
    /// دریافت لیست شعب بر اساس ای دی وب سرویس قبلی
    /// </summary>
    /// <param name="DealerId"></param>
    /// <returns></returns>
    Task<ResultDto<BranchResponse[]>> GetAllBranch(Guid dealerId);
    /// <summary>
    /// لیست جایگاه های فعال
    /// </summary>
    /// <param name="branchId"></param>
    /// <returns></returns>
    Task<ResultDto<AllServerGroupResponse[]>> GetAllServerGroup(Guid branchId);
    /// <summary>
    /// لیست تاریخ های موجود از دو وب سرویس قبلی میاد
    /// </summary>
    /// <param name="ServerGroupId"></param>
    /// <param name="BranchId"></param>
    /// <returns></returns>
    Task<ResultDto<AllServerGroupDateResponse[]>> GetAllServerGroupDate(Guid serverGroupId, Guid branchId);
    /// <summary>
    /// لیست ساعت های موجود
    /// </summary>
    /// <param name="WorkShopTimeTableId"></param>
    /// <param name="ServerGroupId"></param>
    /// <returns></returns>
    Task<ResultDto<AllServerGroupTimeResponse[]>> GetAllServerGroupTime(Guid workShopTimeTableId, Guid serverGroupId);
    /// <summary>
    /// بررسی رزرو باز برای این شاسی وجود دارد یا ندارد در صفحه اولیه نمایش داده شود
    /// </summary>
    /// <param name="VinNumber"></param>
    /// <returns></returns>
    Task<(bool, string)> CheckExistsReserveVinNumber(string vinNumber);
    /// <summary>
    /// ولید بودن مشتری چک شود
    /// </summary>
    /// <param name="VinNumber"></param>
    /// <returns></returns>
    Task<ResultDto<SubscriberChassisAllocationResponse[]>> GetSubscriberChassisAllocation(string vinNumber);
    /// <summary>
    /// بررسی اینکه ایا کلیومتر پذیرش ثبت شده درست است یا خیر نمایش داده میشود در صورت اینکه کیلومتر وارد شده از اطلاعات ثبت شده در سیستم کمتر باشد false  بر میگردونه
    /// </summary>
    /// <param name="VinNumber"></param>
    /// <param name="kilometer"></param>
    /// <returns></returns>
    Task<(bool, string)> CheckIsValidKilometer(string vinNumber, int kilometer);
}

public class SevenSoftService : ISevenSoftService
{
    private string _baseUrl = Infrastucture.Properties.Resource7Soft.BaseUrl;
    public async Task<BranchesByDealerResponse[]> GetBranchesByDealer(Guid dealerId)
    {
        return await RestUtility.GetData<BranchesByDealerResponse[]>(_baseUrl, Resource7Soft.GetBranchesByDealer, dealerId);
    }
    public async Task<ReceptionsInformationByVinNumberResponse[]> GetReceptionsInformationByVinNumber(string chassisVinNumber)
    {
        return await RestUtility.GetData<ReceptionsInformationByVinNumberResponse[]>(_baseUrl, Resource7Soft.GetReceptionsInformationByVinNumber, chassisVinNumber);
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

    public async Task<ResultDto<ReceptionsPartsInformationByReceptionCodeResponse[]>> GetReceptionsPartsInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionsPartsInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionsPartsInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");
            return new ResultDto<ReceptionsPartsInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {

            return new ResultDto<ReceptionsPartsInformationByReceptionCodeResponse[]>(ex.Message, false);
        }

    }

    public async Task<ResultDto<ReceptionsInServicesInformationByReceptionCodeResponse[]>> GetReceptionsInServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionsInServicesInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionsInServicesInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");

            return new ResultDto<ReceptionsInServicesInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {

            return new ResultDto<ReceptionsInServicesInformationByReceptionCodeResponse[]>(ex.Message, false);
        }

    }

    public async Task<ResultDto<ReceptionsOutServicesInformationByReceptionCodeResponse[]>> GetReceptionsOutServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionsOutServicesInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionsOutServicesInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");

            return new ResultDto<ReceptionsOutServicesInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<ReceptionsOutServicesInformationByReceptionCodeResponse[]>(ex.Message, false);
        }
    }

    public async Task<ResultDto<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>> GetReceptionCustomerStatementInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>(_baseUrl, Resource7Soft.GetReceptionCustomerStatementInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");
            return new ResultDto<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>(ex.Message, false);
        }

    }


    public async Task<ResultDto<ReceptionsInformationByReceptionIDResponse>> GetReceptionsInformationByReceptionID(string ReceptionCode)
    {
        try
        {
            var data = await RestUtility.PostData<ReceptionsInformationByReceptionIDResponse>(_baseUrl, Resource7Soft.getReceptionsInformationByReceptionID, ReceptionCode);
            return new ResultDto<ReceptionsInformationByReceptionIDResponse>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<ReceptionsInformationByReceptionIDResponse>(ex.Message, false);
        }
    }


    public async Task<ResultDto<SubCountryResponse[]>> GetAllSubCountries(Guid CountryId)
    {
        try
        {
            var data = await RestUtility.GetData<SubCountryResponse[]>(_baseUrl, Resource7Soft.GetAllSubCountries, $"?CountryId={CountryId}");
            return new ResultDto<SubCountryResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<SubCountryResponse[]>(ex.Message, false);
        }
    }
    public async Task<ResultDto<CityResponse[]>> GetAllCity(Guid SubCountryId)
    {
        try
        {
            var data = await RestUtility.GetData<CityResponse[]>(_baseUrl, Resource7Soft.GetAllCity, $"?SubCountryId={SubCountryId}");
            return new ResultDto<CityResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<CityResponse[]>(ex.Message, false);
        }
    }

    public async Task<ResultDto<SubscriberChassisAllocationResponse[]>> GetSubscriberChassisAllocation(string VinNumber)
    {
        try
        {
            var data = await RestUtility.GetData<SubscriberChassisAllocationResponse[]>(_baseUrl, Resource7Soft.GetSubscriberChassisAllocation, $"?VinNumber={VinNumber}");
            return new ResultDto<SubscriberChassisAllocationResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<SubscriberChassisAllocationResponse[]>(ex.Message, false);
        }
    }
    public async Task<(bool,string)> CheckExistsReserveVinNumber(string VinNumber)
    {
        try
        {
            var data = await RestUtility.GetData<bool>(_baseUrl, Resource7Soft.CheckExistsReserveVinNumber, $"?VinNumber={VinNumber}");
            return new (true,"");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }
    public async Task<(bool, string)> CheckIsValidKilometer(string VinNumber,int kilometer)
    {
        try
        {
            var data = await RestUtility.GetData<bool>(_baseUrl, Resource7Soft.CheckIsValidKilometer, $"?VinNumber={VinNumber}&Kilometer={kilometer}");
            return new(true, "");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }

    

    public async Task<ResultDto<AllServerGroupTimeResponse[]>> GetAllServerGroupTime(Guid WorkShopTimeTableId,Guid ServerGroupId)
    {
        try
        {
            var data = await RestUtility.GetData<AllServerGroupTimeResponse[]>(_baseUrl, Resource7Soft.GetAllServerGroupTime, $"?WorkShopTimeTableId={WorkShopTimeTableId}&ServerGroupId={ServerGroupId}");
            return new ResultDto<AllServerGroupTimeResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<AllServerGroupTimeResponse[]>(ex.Message, false);
        }
    }
    public async Task<ResultDto<AllServerGroupDateResponse[]>> GetAllServerGroupDate(Guid ServerGroupId,Guid BranchId)
    {
        try
        {
            var data = await RestUtility.GetData<AllServerGroupDateResponse[]>(_baseUrl, Resource7Soft.GetAllServerGroupDate, $"?ServerGroupId={ServerGroupId}&BranchId={BranchId}");
            return new ResultDto<AllServerGroupDateResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<AllServerGroupDateResponse[]>(ex.Message, false);
        }
    }
    public async Task<ResultDto<AllServerGroupResponse[]>> GetAllServerGroup(Guid branchId)
    {
        try
        {
            var data = await RestUtility.GetData<AllServerGroupResponse[]>(_baseUrl, Resource7Soft.GetAllServerGroup, $"?branchId={branchId}");
            return new ResultDto<AllServerGroupResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<AllServerGroupResponse[]>(ex.Message, false);
        }
    }
    public async Task<ResultDto<BranchResponse[]>> GetAllBranch(Guid DealerId)
    {
        try
        {
            var data = await RestUtility.GetData<BranchResponse[]>(_baseUrl, Resource7Soft.GetAllBranch, $"?DealerId={DealerId}");
            return new ResultDto<BranchResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<BranchResponse[]>(ex.Message, false);
        }
    }

    public async Task<ResultDto<DealerResponse[]>> GetAllDealer(Guid CityId)
    {
        try
        {
            var data = await RestUtility.GetData<DealerResponse[]>(_baseUrl, Resource7Soft.GetAllDealer, $"?CityId={CityId}");
            return new ResultDto<DealerResponse[]>("", true, data);
        }
        catch (Exception ex)
        {
            return new ResultDto<DealerResponse[]>(ex.Message, false);
        }
    }
}
 
 