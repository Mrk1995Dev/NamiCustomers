namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class GetAllServerGroupResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string ServerGroupName { get; set; }
    public string ServerGroupLocalizedName { get; set; }
    public object Description { get; set; }
    public bool AllVehicleModel { get; set; }
    public object[] Servers { get; set; }
    public int Order { get; set; }
}
