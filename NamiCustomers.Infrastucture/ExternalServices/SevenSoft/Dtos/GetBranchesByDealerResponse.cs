namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;


public class GetBranchesByDealerResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string EconomicCode { get; set; }
    public object NationalId { get; set; }
    public string NationalCode { get; set; }
    public bool IsForeignNational { get; set; }
    public string DealerName { get; set; }
    public object DealerNo { get; set; }
    public object DealerId { get; set; }
    public string BranchNo { get; set; }
    public string BranchName { get; set; }
    public string ManagerName { get; set; }
    public object Subscriber { get; set; }
    public object SubscriberId { get; set; }
    public string BranchGrade { get; set; }
    public string DefualtDeliveryBranchId { get; set; }
    public string DefualtDeliveryBranchName { get; set; }
    public string CityName { get; set; }
    public string SubCountryName { get; set; }
    public string BranchStatus { get; set; }
    public string FinancialSituationName { get; set; }
    public string WeekDayPresenceStatus { get; set; }
    public string HolidayPresenceStatus { get; set; }
    public object ThursdayPresenceStatus { get; set; }
    public string WeekDayStartTime { get; set; }
    public object HolidayStartTime { get; set; }
    public object ThursdayStartTime { get; set; }
    public string WeekDayEndTime { get; set; }
    public object HolidayEndTime { get; set; }
    public object ThursdayEndTime { get; set; }
    public object SaleDealerNo { get; set; }
    public bool IsCentralBranch { get; set; }
    public bool IsLegal { get; set; }
    public int LegalEnum { get; set; }
    public int AreaCategoryId { get; set; }
    public string AreaCategoryName { get; set; }
    public string CompanyInfoId { get; set; }
    public string CompanyInfo { get; set; }
    public object RepairShopAddress { get; set; }
    public object RepairShopPhoneNumber { get; set; }
    public object RepairShopPostalCode { get; set; }
    public object RepairShopFax { get; set; }
    public object Latitude { get; set; }
    public object Longitude { get; set; }
    public object TelPhone { get; set; }
    public object Email { get; set; }
    public object IDCode { get; set; }
    public bool IsActiveBooking { get; set; }
    public object CityId { get; set; }
    public object SubCountryId { get; set; }
    public object SubCountryCode { get; set; }
    public object CityCode { get; set; }
    public object BranchId { get; set; }
    public object FilterCityRow { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}
