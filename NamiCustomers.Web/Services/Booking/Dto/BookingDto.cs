namespace NamiCustomers.Web.Services.Booking.Dto;

public class BookingDto
{
    public string? UniqueId { get; set; }
    public string? BookingCode { get; set; }
    public string? BookingStatus { get; set; }
    public int BookingStatusId { get; set; }
    public string? StrBookingDate { get; set; }
    public string? StrBookingTime { get; set; }
    public DateTime? BookingDate { get; set; }
    public string? DealerName { get; set; }
    public string? BranchName { get; set; }
    public string? BookingDealerAddress { get; set; }
    public string? BookingListFullAddress { get; set; }
    public string? BranchShopAddress { get; set; }
    public string? BranchShopPhoneNumber { get; set; }
    public int BookingKilometer { get; set; }
    public string? BookingRepairAdviser { get; set; }
    public string? PersonnelReception { get; set; }
    public string? BookingVehicleModel { get; set; }
    public string? BookingVehicleModelName { get; set; }
    public string? AbreviatedName { get; set; }
    public string? BookingVehicleTypeName { get; set; }
    public string? BookingVinNumber { get; set; }
    public string? Last8DigitsOfChassisNo { get; set; }
    public string? BookingChassisPlate { get; set; }
    public string? BookingFollowup { get; set; }
    public string? BookingServerGroupLocalizedName { get; set; }
    public string? BookingListRepairPlace { get; set; }
    public string? Location { get; set; }
    public string? BookingSubscriberName { get; set; }
    public string? BookingFirstName { get; set; }
    public string? BookingLastName { get; set; }
    public object? BookingMobile { get; set; }
    public object? BookingDescription { get; set; }
    public BookingCustomerStatementDto[]? BookingCustomerStatement { get; set; }
}

public class BookingCustomerStatementDto
{
    public string? BookingCustomerStatementDescription { get; set; }
    public string? CustomerStatementsTypeLocalizedName { get; set; }
}
