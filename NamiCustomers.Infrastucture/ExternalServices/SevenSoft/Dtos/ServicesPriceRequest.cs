using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class ServicesPriceRequest
{
    public Guid? VehicleModelId { get; set; }
    [DisplayName("کد خدمت")]
    public string? ServiceCode { get; set; }
    [DisplayName("عنوان خدمت")]
    public string? ServiceName { get; set; }
    public string ChassisVinNumber { get; set; } = null!;
    public string NationalCodeOrEconomicCode { get; set; } = null!;
    [DisplayName("شعبه")]
    public Guid BranchId { get; set; }
    [DisplayName("نمایندگی")]
    public Guid DealerId { get; set; }

}

public class ServicesPriceResponse
{
    [DisplayName("ردیف")]
    public int RowNumber { get; set; }
    [DisplayName("کد خدمت")]
    public string ServiceCode { get; set; }
    [DisplayName("عنوان خدمت")]
    public string ServiceName { get; set; }
    [DisplayName("قیمت")]
    public float Price { get; set; }
    [DisplayName("شعبه")]
    public Guid BranchId { get; set; }
    [DisplayName("قیمت برای مشتری")]
    public float PriceCustomer { get; set; }
    [DisplayName("قیمت گارانتی")]
    public float PriceWarranty { get; set; }
}
