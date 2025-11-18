using System.ComponentModel;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class PartsPriceByChassisRequest
{
    [DisplayName("شناسه مدل خودرو")]
    public string VehicleModelId { get; set; }
    [DisplayName("شماره قطعه")]
    public string? PartNo { get; set; }
    [DisplayName("شماره شاسی")]
    public string ChassisVinNumber { get; set; }
    [DisplayName("شناسه ملی")]
    public string NationalCodeOrEconomicCode { get; set; }
    [DisplayName("نام قطعه")]
    public string? PartName { get; set; }
    [DisplayName("شماره تامین کننده")]
    public object PartSupplierNo { get; set; }
}
