namespace NamiCustomers.Web.Services.Vehicle.Dto;

public class PartsPriceByChassisResponse
{
    public int RowNumber { get; set; }
    public string PartNo { get; set; }
    public string PartName { get; set; }
    public PartsPriceItemResponse[] Prices { get; set; }
    public float Price { get; set; }
    public float PriceByTax => Price + ((int)(Price * 10) / 100);
    public object PartSupplierNo { get; set; }
}