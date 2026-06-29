using System.ComponentModel;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class PartsPriceByChassisResponse
{
    [DisplayName("شماره ردیف")]
    public int RowNumber { get; set; }
    [DisplayName("شماره قطعه")]
    public string PartNo { get; set; }
    [DisplayName("نام قطعه")]
    public string PartName { get; set; }
    [DisplayName("قیمت های قطعه")]
    public PartsPriceItemResponse[] Prices { get; set; }
    [DisplayName("قیمت قطعه (بدون ارزش افزوده ) ")]
    public float Price { get; set; }

    [DisplayName("قیمت قطعه (با احتساب ارزش افزوده ) ")]
    public float PriceByTax =>Price + ((int)(Price * 10) / 100);
    public object PartSupplierNo { get; set; }
}