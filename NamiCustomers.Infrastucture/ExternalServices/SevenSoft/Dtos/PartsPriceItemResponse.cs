namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class PartsPriceItemResponse
{
    public int Code { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public bool Active { get; set; }
    public int SubscriberTypeId { get; set; }
}
