namespace NamiCustomers.Domain.Entities.Vehicles;
[Auditable]
public class VehicleModel : IBaseEntity<int>
{
    public int Id { get; set; }
    public int? SubscriberId { get; set; }
    public string? ProductYear { get; set; }
    public string? BodyColor { get; set; }
    public string? MotorNumber { get; set; }
    public string? FullSystem { get; set; }
    public Guid? VehicleModelIdSevenSoft { get; set; }
    public string? ChassisUsageTypeName { get; set; }
    //public  string? Description { get; set; }
    public string? SelectedVehicleCommonName { get; set; }
    public string? SelectedVehicleDescription { get; set; }
    /// <summary>
    /// شماره شاسی 
    /// </summary>
    public string? VinNumber { get; set; }
    //public Guid? SalePlanIdSevenSoft { get; set; }
    //public Guid? SaleBasketIdSevenSoft { get; set; }
    //public Guid? BrandIdSevenSoft { get; set; }
    public VehicleAttachment? VehicleAttachment { get; set; }
    public bool IsDefault { get; set; } = false;
}
