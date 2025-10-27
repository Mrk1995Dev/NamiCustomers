namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
 
public class SubscriberChassisAllocationResponse
{
    public int Code { get; set; }
    public Guid UniqueId { get; set; }
    public object ChassisPlate { get; set; }
    public Chassisplatevm ChassisPlateVm { get; set; }
    public string SubscriberName { get; set; }
    public string SubscriberFirstName { get; set; }
    public string SubscriberLastName { get; set; }
    public string SubscriberHomeTel { get; set; }
    public string SubscriberMobile { get; set; }
    public object BookingId { get; set; }
    public object BodyColorId { get; set; }
    public object ColorName { get; set; }
    public string SubscriberEmail { get; set; }
    public string SubscriberId { get; set; }
    public string SubscriberNationalCode { get; set; }
    public string SubscriberEconomicCode { get; set; }
    public string SubscriberNationalId { get; set; }
    public object SubscriberTypeId { get; set; }
    public string ChassisInfoId { get; set; }
    public string ChassisVinNumber { get; set; }
    public object OriginalVinNumber { get; set; }
    public object ChassisMotorNumber { get; set; }
    public DateTime StartDate { get; set; }
    public string StrStartDate { get; set; }
    public bool Active { get; set; }
    public string ChassisVehicleModelName { get; set; }
    public string ChassisVehicleModelId { get; set; }
    public string SubscriberFullName { get; set; }
    public string SubscriberAddress { get; set; }
    public object SubscriberMorality { get; set; }
    public string ChassisVehicleName { get; set; }
    public bool IsLegalSubscriber { get; set; }
    public string SubscriberCityId { get; set; }
    public object ChassisInfoSpecificCases { get; set; }
    public int ChassisPlatePartOne { get; set; }
    public object ChassisPlatePartTwo { get; set; }
    public int ChassisPlatePartTwoInt { get; set; }
    public int ChassisPlatePartThree { get; set; }
    public int ChassisPlatePartFour { get; set; }
    public int ChassisPlatePartFive { get; set; }
    public int ChassisPlatePartSix { get; set; }
    public bool IsForeignNational { get; set; }
    public object Description { get; set; }
    public DateTime ChassisMainGuarantyExpireDate { get; set; }
    public string StrChassisMainGuarantyExpireDate { get; set; }
    public object AbreviatedCode { get; set; }
    public object BodyColor { get; set; }
    public object InternalColor { get; set; }
    public DateTime ChassisMainGuarantyRegisterDate { get; set; }
    public string StrChassisMainGuarantyRegisterDate { get; set; }
    public bool ChassisMainGuarantyActive { get; set; }
    public int ReceptionListLastKilometer { get; set; }
    public object BookingKilometer { get; set; }
    public object BookingClock { get; set; }
    public object UnusedReceptionLicensesMessage { get; set; }
    public bool WithoutPlate { get; set; }
    public object BookingWarrantyStatus { get; set; }
    public object BookingLastReceptionServiceName { get; set; }
    public object BookingLastReceptionDate { get; set; }
    public object StrBookingLastReceptionDate { get; set; }
    public int BookingReceptionListLastKilometer { get; set; }
    public object ProductionDate { get; set; }
    public string StrProductionDate { get; set; }
    public object VehicleSaleDeliveryDate { get; set; }
    public string StrVehicleSaleDeliveryDate { get; set; }
    public object VehicleTypeName { get; set; }
    public string VehicleTypeId { get; set; }
    public object VehicleName { get; set; }
    public string VehicleId { get; set; }
    public object BrandName { get; set; }
    public object BrandId { get; set; }
    public bool IsCompanymortgage { get; set; }
    public object ProductYear { get; set; }
    public object Kilometer { get; set; }
    public bool ChassisInActive { get; set; }
    public string EconomicCodeOrNationalCode { get; set; }
    public bool VehicleTypeRecClockIsRequired { get; set; }
    public bool AddReceptionRecClockIsRequired { get; set; }
    public object ChassisVehicleFullEnName { get; set; }
    public object ChassisVehicleFullPeName { get; set; }
    public object ChassisBodyNumber { get; set; }
    public object ChassisVehicleModelCreateOn { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Chassisplatevm
{
    public int PartOnek__BackingField { get; set; }
    public int PartTwok__BackingField { get; set; }
    public int PartThreek__BackingField { get; set; }
    public int PartFourk__BackingField { get; set; }
    public int PartFivek__BackingField { get; set; }
    public int PartSixk__BackingField { get; set; }
    public int VehicleUsageTypek__BackingField { get; set; }
}

