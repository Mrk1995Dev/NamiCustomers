using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos
{

    public class InsertBookingRequest
    {
        public object UniqueId { get; set; }
        public int Code { get; set; }
        public bool BookingIsCompanyCustomer { get; set; }
        public Guid BookingSubscriberChassisAllocationId { get; set; }
        public string BookingFirstName { get; set; }
        public string BookingLastName { get; set; }
        public string BookingVinNumber { get; set; }
        public Guid BookingVehicleModelId { get; set; }
        public int BookingKilometer { get; set; }
        public bool CheckBookingKilometer { get; set; }
        public string? BookingTime { get; set; }
        public string BookingDescription { get; set; }
        public string BookingHomeTel { get; set; }
        public string BookingOfficeTel { get; set; }
        public string BookingEmail { get; set; }
        public string BookingMobile { get; set; }
        public Guid? BookingServerGroupId { get; set; }
        public Bookingcustomerstatementlist[] BookingCustomerStatementList { get; set; }
        public object[] BookingCustomerStatementDescriptionList { get; set; }
        public Guid? BookingRepairAdviserId { get; set; }
        public Guid? WorkShopTimeTableId { get; set; }
        public Selectedbookingrequesttype SelectedBookingRequestType { get; set; }
        public bool AddBookinglock { get; set; }
        public int AddBookingRepairPlaceEnum { get; set; }
        public Guid? BranchId { get; set; }
        public string FullAddress { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsBookingFinal { get; set; }
    }

    public class Selectedbookingrequesttype
    {
        public string[] CheckBoxIds { get; set; }
    }

    public class Bookingcustomerstatementlist
    {
        public string BookingCustomerStatementDescription { get; set; }
        public object CustomerStatementsTypeId { get; set; }
        public object DefaultCustomerDescriptionId { get; set; }
        public string DefaultCustomerDescription { get; set; }
        public bool Approved { get; set; }
        public object CustomerStatementsTypeName { get; set; }
        public string DefaultCustomerDescriptionName { get; set; }
    }


}







