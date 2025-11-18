namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class AllOrderStatusTypeResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string OrderStatusTypeLocalizedName { get; set; }
    public string OrderStatusTypeName { get; set; }
    public object Description { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}
