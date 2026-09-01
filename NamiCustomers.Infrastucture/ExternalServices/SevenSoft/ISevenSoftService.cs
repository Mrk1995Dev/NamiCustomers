using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;
using NamiCustomers.Infrastucture.Utilities;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft;

public interface ISevenSoftService
{

    /// <summary>
    /// گرفتن لیست کارشناسان
    /// </summary>
    /// <param name="WorkShopTimeTableId"></param>
    /// <param name="werverGroupId"></param>
    /// <returns></returns>
    Task<List<RepairAdvisersResponse>> GetAllServerGroupRepairAdvisers(Guid? workShopTimeTableId, Guid? serverGroupId);
    /// <summary>
    /// ثبت نوبت
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ResultDto<InsertBookingResponse>> InsertBooking(InsertBookingRequest request);
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
    Task<string[]> GetSpecificCases(string? chassisVinNumber, string? nationalCodeOrEconomicCode, string? mobile);
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
    Task<ResultDto<SubscriberChassisAllocationResponse>> GetSubscriberChassisAllocation(string vinNumber);
    /// <summary>
    /// بررسی اینکه ایا کلیومتر پذیرش ثبت شده درست است یا خیر نمایش داده میشود در صورت اینکه کیلومتر وارد شده از اطلاعات ثبت شده در سیستم کمتر باشد false  بر میگردونه
    /// </summary>
    /// <param name="VinNumber"></param>
    /// <param name="kilometer"></param>
    /// <returns></returns>
    Task<(bool, string)> CheckIsValidKilometer(string vinNumber, int kilometer);
    /// <summary>
    /// انصراف از نوبت
    /// </summary>
    /// <param name="bookingId"></param>
    /// <param name="vinNumber"></param>
    /// <returns></returns>
    Task<bool> CancelBooking(Guid bookingId, string vinNumber);
    /// <summary>
    /// مشاهده لیست رزرو اینترنت
    /// </summary>
    /// <param name="vinNumber"></param>
    /// <returns></returns>
    Task<ResultDto<BookingResponse>> GetBooking(string vinNumber);
    Task<ChassisInformationResponse[]> GetAllChassisInformation(string nationalCode);
    /// <summary>
    /// قطعات
    /// </summary>
    /// <param name="servicesPriceRequest"></param>
    /// <returns></returns>
    Task<ResultDto<ServicesPriceResponse[]>> GetServicesPriceByBranchId(ServicesPriceRequest servicesPriceRequest);
    /// <summary>
    /// دریافت لیست سفارشات
    /// </summary>
    /// <param name="ChassisVinNumber"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    Task<SpOrderingsBySubscriberResponse[]> GetSpOrderingsBySubscriber(string ChassisVinNumber, string NationalCodeOrEconomicCode);
    /// <summary>
    /// ریز سفارشات
    /// </summary>
    /// <param name="SpOrderingCode"></param>
    /// <returns></returns>
    Task<SpOrderingPartSpOrderingCodeResponse[]> GetSpOrderingPartSpOrderingCode(string SpOrderingCode);
    /// <summary>
    /// دریافت لیست قیمت قطعات
    /// </summary>
    /// <param name="getPartsPriceByChassisRequest"></param>
    /// <returns></returns>
    Task<ResultDto<PartsPriceByChassisResponse[]>> GetPartsPriceByChassis(PartsPriceByChassisRequest getPartsPriceByChassisRequest);
    /// <summary>
    /// وضعیت سفارشات
    /// </summary>
    /// <returns></returns>
    Task<AllOrderStatusTypeResponse[]> GetAllOrderStatusType();

    Task<(bool, string)> CheckExistsReservePhoneNumber(string phoneNumber);
}

public class SevenSoftService : ISevenSoftService
{
    
    public async Task<ResultDto<PartsPriceByChassisResponse[]>> GetPartsPriceByChassis(PartsPriceByChassisRequest getPartsPriceByChassisRequest)
    {
        try
        {
            var data = await RestUtility.PostData<PartsPriceByChassisResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetPartsPriceByChassis, getPartsPriceByChassisRequest);
            return ResultDto.Success<PartsPriceByChassisResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<PartsPriceByChassisResponse[]>(ex.Message);
        }
    }
    public async Task<AllOrderStatusTypeResponse[]> GetAllOrderStatusType()
    {
        return await RestUtility.GetData<AllOrderStatusTypeResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetAllOrderStatusTypeResponse, "");
    }

    public async Task<SpOrderingPartSpOrderingCodeResponse[]> GetSpOrderingPartSpOrderingCode(string SpOrderingCode)
    {
        return await RestUtility.GetData<SpOrderingPartSpOrderingCodeResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, string.Format(Resource7Soft.GetSpOrderingPartSpOrderingCode, SpOrderingCode), "");
    }

    public async Task<SpOrderingsBySubscriberResponse[]> GetSpOrderingsBySubscriber(string ChassisVinNumber, string NationalCodeOrEconomicCode)
    {
        return await RestUtility.GetData<SpOrderingsBySubscriberResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, string.Format(Resource7Soft.GetSpOrderingsBySubscriber, ChassisVinNumber, NationalCodeOrEconomicCode), "");
    }


    public async Task<List<RepairAdvisersResponse>> GetAllServerGroupRepairAdvisers(Guid? workShopTimeTableId, Guid? serverGroupId)
    {
        return await RestUtility.GetData<List<RepairAdvisersResponse>>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, string.Format(Resource7Soft.GetAllServerGroupRepairAdvisers, workShopTimeTableId, serverGroupId), "");
    }
    public async Task<BranchesByDealerResponse[]> GetBranchesByDealer(Guid dealerId)
    {
        return await RestUtility.GetData<BranchesByDealerResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetBranchesByDealer, dealerId);
    }
    public async Task<ReceptionsInformationByVinNumberResponse[]> GetReceptionsInformationByVinNumber(string chassisVinNumber)
    {
        return await RestUtility.GetData<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetReceptionsInformationByVinNumber, chassisVinNumber);
    }


    public async Task<ResultDto<ServicesPriceResponse[]>> GetServicesPriceByBranchId(ServicesPriceRequest servicesPriceRequest)
    {
        try
        {
            var data = await RestUtility.PostData<ServicesPriceResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetServicesPriceByBranchId, servicesPriceRequest);
            return ResultDto.Success<ServicesPriceResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<ServicesPriceResponse[]>(ex.Message);
        }
    }
    public async Task<DealerResponse[]> GetDealers()
    {
        return await RestUtility.GetData<DealerResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetDealers, "");
    }
    public async Task<ActiveMainChassisGuaranteeResponse> GetActiveMainChassisGuarantee(string vinNumber)
    {
        return await RestUtility.GetData<ActiveMainChassisGuaranteeResponse>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetActiveMainChassisGuarantee, vinNumber);
    }
    public async Task<SevenSubscriberResponse> GetSubscriberByNationalCode(string nationalCode)
    {
        return await RestUtility.GetData<SevenSubscriberResponse>(Infrastucture.Properties.Resource7Soft.BaseUrlSales, Resource7Soft.GetSubscriberByNationalCode, nationalCode);
    }
    public async Task<ChassisInformationResponse[]> GetAllChassisInformation(string nationalCode)
    {
        return await RestUtility.GetData<ChassisInformationResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, string.Format(Resource7Soft.GetAllChassisInformation, nationalCode), "");
    }


    public async Task<ResultDto<InsertBookingResponse>> InsertBooking(InsertBookingRequest request)
    {
        try
        {
            var data = await RestUtility.PostData<InsertBookingResponse>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.InsertBooking, request);
            return   ResultDto.Success<InsertBookingResponse>(data);
        }
        catch (Exception ex)
        {
            return  ResultDto.Failure<InsertBookingResponse>(ex.Message);
        }
    }


    public async Task<ChassisInformationByVinNumberResponse> GetChassisInformationByVinNumber(string vinNumber)
    {
        return await RestUtility.GetData<ChassisInformationByVinNumberResponse>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetChassisInformationByVinNumber, vinNumber);
    }
    public async Task<string> GetRelationCustomerInfoByVinNumber(string chassisVinNumber, string nationalCodeOrEconomicCode, string mobile)
    {
        string qStr = $"?ChassisVinNumber={chassisVinNumber}&NationalCodeOrEconomicCode={nationalCodeOrEconomicCode}&Mobile={mobile}";
        return await RestUtility.GetData<string>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.RelationCustomerInfoByVinNumber, qStr);
    }

    public async Task<string[]> GetSpecificCases(string? chassisVinNumber, string? nationalCodeOrEconomicCode, string? mobile)
    {
        string qStr = $"?ChassisVinNumber={chassisVinNumber}&NationalCodeOrEconomicCode={nationalCodeOrEconomicCode}&Mobile={mobile}";
        return await RestUtility.GetData<string[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetSpecificCases, qStr);
    }

    public async Task<ResultDto<ReceptionsPartsInformationByReceptionCodeResponse[]>> GetReceptionsPartsInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionsPartsInformationByReceptionCodeResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetReceptionsPartsInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");
            return   ResultDto.Success<ReceptionsPartsInformationByReceptionCodeResponse[]>(data);
        }
        catch (Exception ex)
        {

            return   ResultDto.Failure<ReceptionsPartsInformationByReceptionCodeResponse[]>(ex.Message);
        }

    }

    public async Task<ResultDto<ReceptionsInServicesInformationByReceptionCodeResponse[]>> GetReceptionsInServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionsInServicesInformationByReceptionCodeResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetReceptionsInServicesInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");

            return ResultDto.Success<ReceptionsInServicesInformationByReceptionCodeResponse[]>(data);
        }
        catch (Exception ex)
        {

            return   ResultDto.Failure<ReceptionsInServicesInformationByReceptionCodeResponse[]>(ex.Message);
        }

    }

    public async Task<ResultDto<ReceptionsOutServicesInformationByReceptionCodeResponse[]>> GetReceptionsOutServicesInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionsOutServicesInformationByReceptionCodeResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetReceptionsOutServicesInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");

            return ResultDto.Success<ReceptionsOutServicesInformationByReceptionCodeResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<ReceptionsOutServicesInformationByReceptionCodeResponse[]>(ex.Message);
        }
    }

    public async Task<ResultDto<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>> GetReceptionCustomerStatementInformationByReceptionCode(string ReceptionCode, string NationalCodeOrEconomicCode)
    {
        try
        {
            var data = await RestUtility.GetData<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.GetReceptionCustomerStatementInformationByReceptionCode, $"?ReceptionCode={ReceptionCode}&NationalCodeOrEconomicCode={NationalCodeOrEconomicCode}");
            return ResultDto.Success<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<ReceptionCustomerStatementInformationByReceptionCodeResponse[]>(ex.Message);
        }

    }


    public async Task<ResultDto<ReceptionsInformationByReceptionIDResponse>> GetReceptionsInformationByReceptionID(string ReceptionCode)
    {
        try
        {
            var data = await RestUtility.PostData<ReceptionsInformationByReceptionIDResponse>(Infrastucture.Properties.Resource7Soft.BaseUrlCrm, Resource7Soft.getReceptionsInformationByReceptionID, ReceptionCode);
            return ResultDto.Success<ReceptionsInformationByReceptionIDResponse>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<ReceptionsInformationByReceptionIDResponse>(ex.Message);
        }
    }


    public async Task<ResultDto<SubCountryResponse[]>> GetAllSubCountries(Guid CountryId)
    {
        try
        {
            var data = await RestUtility.GetData<SubCountryResponse[]>(Resource7Soft.BaseUrlBooking, Resource7Soft.GetAllSubCountries, $"?CountryId={CountryId}");
            return ResultDto.Success<SubCountryResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<SubCountryResponse[]>(ex.Message);
        }
    }
    public async Task<ResultDto<CityResponse[]>> GetAllCity(Guid SubCountryId)
    {
        try
        {
            var data = await RestUtility.GetData<CityResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.GetAllCity, $"?SubCountryId={SubCountryId}");
            return ResultDto.Success<CityResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<CityResponse[]>(ex.Message);
        }
    }

    public async Task<ResultDto<SubscriberChassisAllocationResponse>> GetSubscriberChassisAllocation(string VinNumber)
    {
        try
        {
            var data = await RestUtility.GetData<SubscriberChassisAllocationResponse>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.GetSubscriberChassisAllocation, $"?VinNumber={VinNumber}");


            return ResultDto.Success<SubscriberChassisAllocationResponse>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<SubscriberChassisAllocationResponse>(ex.Message);
        }
    }
    public async Task<(bool, string)> CheckExistsReserveVinNumber(string VinNumber)
    {
        try
        {
            var data = await RestUtility.GetData<bool>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.CheckExistsReserveVinNumber, $"?VinNumber={VinNumber}");
            return new(data, "");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }

    public async Task<(bool, string)> CheckExistsReservePhoneNumber(string phoneNumber)
    {
        try
        {
            var data = await RestUtility.GetData<bool>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.CheckExistsReserveByMobileNumber, $"?mobileNumber={phoneNumber}");
            return new(data, "");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }

    public async Task<(bool, string)> CheckIsValidKilometer(string VinNumber, int kilometer)
    {
        try
        {
            var data = await RestUtility.GetData<bool>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.CheckIsValidKilometer, $"?VinNumber={VinNumber}&Kilometer={kilometer}");
            return new(true, "");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }
    public async Task<ResultDto<BookingResponse>> GetBooking(string vinNumber)
    {
        try
        {
            var data = await RestUtility.GetData<BookingResponse>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, string.Format(Resource7Soft.GetBooking, vinNumber), "");
            if (data != null)
            {
                return   ResultDto.Success<BookingResponse>(data);
            }
            return   ResultDto.Failure<BookingResponse>(Infrastucture.Properties.Resources.errNotFound);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<BookingResponse>(ex.Message);
        }
    }
    public async Task<bool> CancelBooking(Guid bookingId, string number)
    {
        using var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Infrastucture.Properties.Resource7Soft.BaseUrlBooking}{string.Format(Resource7Soft.CancelBooking, bookingId, number)}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsStringAsync();
            return bool.Parse(res);
        }
        return false;
    }
    public async Task<ResultDto<AllServerGroupTimeResponse[]>> GetAllServerGroupTime(Guid WorkShopTimeTableId, Guid ServerGroupId)
    {
        try
        {
            var data = await RestUtility.GetData<AllServerGroupTimeResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.GetAllServerGroupTime, $"?WorkShopTimeTableId={WorkShopTimeTableId}&ServerGroupId={ServerGroupId}");
            return   ResultDto.Success<AllServerGroupTimeResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<AllServerGroupTimeResponse[]>(ex.Message);
        }
    }
    public async Task<ResultDto<AllServerGroupDateResponse[]>> GetAllServerGroupDate(Guid ServerGroupId, Guid BranchId)
    {
        try
        {
            var data = await RestUtility.GetData<AllServerGroupDateResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.GetAllServerGroupDate, $"?ServerGroupId={ServerGroupId}&BranchId={BranchId}");
            return ResultDto.Success<AllServerGroupDateResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<AllServerGroupDateResponse[]>(ex.Message);
        }
    }
    public async Task<ResultDto<AllServerGroupResponse[]>> GetAllServerGroup(Guid branchId)
    {
        try
        {
            var data = await RestUtility.GetData<AllServerGroupResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.GetAllServerGroup, $"?branchId={branchId}");
            return   ResultDto.Success<AllServerGroupResponse[]>(data);
        }
        catch (Exception ex)
        {
            return   ResultDto.Failure<AllServerGroupResponse[]>(ex.Message);
        }
    }
    public async Task<ResultDto<BranchResponse[]>> GetAllBranch(Guid DealerId)
    {
        try
        {
            var data = await RestUtility.GetData<BranchResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.GetAllBranch, $"?DealerId={DealerId}");
            return ResultDto.Success<BranchResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<BranchResponse[]>(ex.Message);
        }
    }

    public async Task<ResultDto<DealerResponse[]>> GetAllDealer(Guid CityId)
    {
        try
        {
            var data = await RestUtility.GetData<DealerResponse[]>(Infrastucture.Properties.Resource7Soft.BaseUrlBooking, Resource7Soft.GetAllDealer, $"?CityId={CityId}");
            return ResultDto.Success<DealerResponse[]>(data);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<DealerResponse[]>(ex.Message);
        }
    }
}

