using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class BookingResponse
{
public string UniqueId { get; set; }
public int Code { get; set; }
public string BookingListRepairPlace { get; set; }
public int BookingListRepairPlaceId { get; set; }
public bool BookingIsCompanyCustomer { get; set; }
public string BookingSubscriberChassisAllocationId { get; set; }
public string BookingSubscriberId { get; set; }
public string BookingFirstName { get; set; }
public string BookingLastName { get; set; }
public object BookingNationalCode { get; set; }
public string BookingVinNumber { get; set; }
public object BookingPersonnelReliefLocation { get; set; }
public object RepairPlaceCode { get; set; }
public string BookingVehicleModelId { get; set; }
public string BookingVehicleModel { get; set; }
public string AbreviatedName { get; set; }
public object BookingVehicleBrand { get; set; }
public object BookingVehicleModelLocalizedName { get; set; }
public object BookingModelYear { get; set; }
public string BookingChassisPlate { get; set; }
public int BookingKilometer { get; set; }
public int BookingListBookingClock { get; set; }
public DateTime BookingDate { get; set; }
public string StrBookingDate { get; set; }
public string StrBookingTime { get; set; }
public string CapacityAllocationType { get; set; }
public int BookingStatusId { get; set; }
public string BookingStatus { get; set; }
public object CanceledPersonnel { get; set; }
public object BookingDescription { get; set; }
public object BookingPersonnelId { get; set; }
public string BookingRepairAdviserId { get; set; }
public string BookingRepairAdviser { get; set; }
public string BookingHomeTel { get; set; }
public object BookingOfficeTel { get; set; }
public string BookingEmail { get; set; }
public object BookingMobile { get; set; }
public string BookingPersonnelMobile { get; set; }
public string BookingPersonnelName { get; set; }
public object BookingSecondMobile { get; set; }
public object BookingListCity { get; set; }
public string BookingListFullAddress { get; set; }
public string BookingDealerAddress { get; set; }
public float BookingListLatitude { get; set; }
public float BookingListLongitude { get; set; }
public string BookingServerGroupId { get; set; }
public string BookingServerGroupLocalizedName { get; set; }
public string DealerId { get; set; }
public string DealerName { get; set; }
public string DealerNo { get; set; }
public string BranchId { get; set; }
public string BranchName { get; set; }
public string BranchNo { get; set; }
public int DealerSystemCode { get; set; }
public int BranchSystemCode { get; set; }
public object BookingCustomerName { get; set; }
public object BookingCustomerAddress { get; set; }
public string BookingCustomerMobile { get; set; }
public object BookingCustomerEmail { get; set; }
public object BookingCustomerNationalCode { get; set; }
public object BookingSubscriberMorality { get; set; }
public string BookingCode { get; set; }
public string ReversedBookingCode { get; set; }
public object[] Package { get; set; }
public object[] BookingRequestTypeDetailList { get; set; }
public Bookingcustomerstatement[] BookingCustomerStatement { get; set; }
public object StrBookingCustomerStatement { get; set; }
public DateTime Date { get; set; }
public string StrDate { get; set; }
public string StrTime { get; set; }
public string BookingStateOrder { get; set; }
public bool BookingLock { get; set; }
public object BookingFollowupId { get; set; }
public string BookingFollowup { get; set; }
public string BranchShopAddress { get; set; }
public string BranchShopPhoneNumber { get; set; }
public string BookingSubscriberName { get; set; }
public object BookingSubscriberHomeTel { get; set; }
public object BookingSubscriberEmail { get; set; }
public float BranchLatitude { get; set; }
public float BranchLongitude { get; set; }
public Chassisplatevm ChassisPlateVm { get; set; }
public string BookingVehicleModelName { get; set; }
public string PersonnelReception { get; set; }
public string TelPersonnelReception { get; set; }
public string Location { get; set; }
public object PersonnelReceptionWorkTell { get; set; }
public object ReceptionWorkRoomAddress { get; set; }
public string RegisteredPersonnelFullName { get; set; }
public string BookingVehicleTypeName { get; set; }
public object CurrentDay { get; set; }
public int Gender { get; set; }
public object DefaultCustomerDescriptionRemarks { get; set; }
public string ClientId { get; set; }
public bool IsDirty { get; set; }
}

public class Bookingcustomerstatement
{
public object Code { get; set; }
public object UniqueId { get; set; }
public string BookingId { get; set; }
public string BookingCustomerStatementDescription { get; set; }
public object CustomerStatementsTypeId { get; set; }
public object DefaultCustomerDescriptionId { get; set; }
public object DefaultCustomerDescriptionLocalizedName { get; set; }
public string CustomerStatementsTypeLocalizedName { get; set; }
public object StatementsTypeId { get; set; }
public object StatementsTypeLocalizedName { get; set; }
public object ExpertTheory { get; set; }
public bool Approved { get; set; }
public object PropblemFinderPersonnelId { get; set; }
public object PropblemFinderPersonnelName { get; set; }
public object PresentBookingDealerSystemCode { get; set; }
public float EstimatedTime { get; set; }
public string ClientId { get; set; }
public bool IsDirty { get; set; }
}