namespace NamiCustomers.Web.Services.Dealer.Dto;

public class DealerResponseDto
{
    public int? Code { get; set; }
    public string? UniqueId { get; set; }
    public object ApprovalOwnerId { get; set; }
    public object ApprovalOwnerAgentName { get; set; }
    public string? DealerName { get; set; }
    public string? DealerNo { get; set; }
    public string? ManagerName { get; set; }
    public string? City { get; set; }
    public bool? CompanyIsOwner { get; set; }
    public string? DealerStatus { get; set; }
    public string? Financialsituation { get; set; }
    public string? WeekDayPresenceStatus { get; set; }
    public string? HolidayPresenceStatus { get; set; }
    public object ThursdayPresenceStatus { get; set; }
    public string? WeekDayStartTime { get; set; }
    public string? StrWeekDayStartTime { get; set; }
    public object HolidayStartTime { get; set; }
    public object StrHolidayStartTime { get; set; }
    public object ThursdayStartTime { get; set; }
    public object StrThursdayStartTime { get; set; }
    public string? WeekDayEndTime { get; set; }
    public string? StrWeekDayEndTime { get; set; }
    public object HolidayEndTime { get; set; }
    public object StrHolidayEndTime { get; set; }
    public object ThursdayEndTime { get; set; }
    public object StrThursdayEndTime { get; set; }
    public object SaleDealerNo { get; set; }
    public string? ServicePercent { get; set; }
    public bool? IsCentralDealer { get; set; }
    public object EconomicCode { get; set; }
    public string? NationalId { get; set; }
    public bool? IsLegal { get; set; }
    public int? LegalEnum { get; set; }
    public int? AreaCategoryId { get; set; }
    public string? RepairShopAddress { get; set; }
    public string? RepairShopPhoneNumber { get; set; }
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
    public string? SubCountry { get; set; }
    public object RepairShopFax { get; set; }
    public object BranchActivityList { get; set; }
    public string? BrancheName { get; set; }
    public string? BranchGrade { get; set; }
    public string? EconomicOrNationalCode { get; set; }
    public bool? CompanyIsBranchOwner { get; set; }
    public bool? BranchActivitys { get; set; }
    public bool? IsActiveBooking { get; set; }
    public Guid? DealerId { get; set; }
    public Guid? BranchId { get; set; }
    public string? ClientId { get; set; }
    public bool? IsDirty { get; set; }
}