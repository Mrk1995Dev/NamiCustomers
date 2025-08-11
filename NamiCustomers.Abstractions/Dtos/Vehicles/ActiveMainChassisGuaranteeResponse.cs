using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Vehicles;

public class ActiveMainChassisGuaranteeResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public DateTime RegisterDateTime { get; set; }
    [Display(Name = "تاریخ شروع گارانتی")]
    public string StrRegisterDateTime { get; set; }
    public DateTime ExpireDateTime { get; set; }
    [Display(Name = "تاریخ پایان گارانتی")]
    public string StrExpireDateTime { get; set; }
    public bool Active { get; set; }
    public object GuarantyTypeId { get; set; }
    [Display(Name = "نوع گارانتی")]
    public string GuarantyTypeName { get; set; }
    public string GuarantyGroupName { get; set; }
    public string GuarantyTypeGroupId { get; set; }
    public string GuarantyTypeGroupName { get; set; }
    public string VinNumber { get; set; }
    [Display(Name = "کیلومتر گارانتی")]
    public int GuaranteeMileage { get; set; }
    [Display(Name = "مدت گارانتی")]
    public int GuaranteeDuration { get; set; }
    public object GuaranteeClock { get; set; }
    public object ChassisGuaranteeDescription { get; set; }
    public bool ReceptionVehicleStatusIsWarranty { get; set; }
    public object GuarantySectionCheckBoxList { get; set; }
    public object GuarantySections { get; set; }
    public string SubscriberChassisAllocationId { get; set; }
    public object ChassisInfoId { get; set; }
    public object SubscriberName { get; set; }
    public object MotorNumber { get; set; }
    public int StartMileage { get; set; }
    public object EndMileage { get; set; }
    public int StartClock { get; set; }
    public object EndClock { get; set; }
    public object OriginalRegisterDateTime { get; set; }
    public object StrOriginalRegisterDateTime { get; set; }
    public object VehicleModel { get; set; }
    public object OldChassisGuaranteeId { get; set; }
    public object OldChassisGuaranteeName { get; set; }
    public bool ContinuityGuaranty { get; set; }
    public bool Reserved { get; set; }
    public object ChassisGuaranteeListExpireDateTime { get; set; }
    public object ChassisGuaranteeListRegisterDateTime { get; set; }
    public object ChassisGuaranteeListClock { get; set; }
    public object ChassisGuaranteeListDuration { get; set; }
    public object ChassisGuaranteeListMileage { get; set; }
    [Display(Name = "گارانتی خودرو فعال است؟")]
    public string ChassisGuaranteeListWarrantyStatus { get; set; }
    public bool ShowInInvoice { get; set; }
    public object VehicleTypeVehicleName { get; set; }
    public object GuaranteeStatusForApi { get; set; }
}
