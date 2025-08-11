namespace NamiCustomers.Domain.Entities.Vehicles;

public class VehicleAttachment : IBaseEntity<int>
{
    public int Id { get; set; }
    public Guid? VehicleModelIdSevenSoft { get; set; }
    public string? ThumbnailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? Guidanc { get; set; }
    public string? Catalog { get; set; }
}
