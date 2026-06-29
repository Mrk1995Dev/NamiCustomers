namespace NamiCustomers.Web.Services.Subscriber.Dto;

public class SubscriberDto
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public string? Phone { get; set; }
    public string NationalCode { get; set; }
    public string Mobile { get; set; } = null!;
    public string? Sex { get; set; }
    public ICollection<VehicleModelDto>? VehicleModels { get; set; }
    public int SubscriberType { get; set; }
    public DateTime? BrithDate { get; set; }
    public string? BrithDatePersian { get; set; }
}

public class VehicleModelDto
{
    public int? Id { get; set; }
    public int? SubscriberId { get; set; }
    public string? VehicleModelName { get; set; }
    public string? VehicleModelLocalizedName { get; set; }
    public string? SelectedVehicleDescription { get; set; }
    public string? ProductYear { get; set; }
    public string? BodyColor { get; set; }
    public string? MotorNumber { get; set; }
    public string? FullSystem { get; set; }
    public Guid? VehicleModelIdSevenSoft { get; set; }
    public string? ChassisUsageTypeName { get; set; }
    public bool IsDefault { get; set; }
    public string? SelectedVehicleCommonName { get; set; }
    public string? VinNumber { get; set; }
    public string NationalCode { get; set; }
    public string Mobile { get; set; }
    public VehicleAttachmentDto? VehicleAttachment { get; set; }
}

public class VehicleAttachmentDto
{
    public int Id { get; set; }
    public Guid? VehicleModelIdSevenSoft { get; set; }
    public string? ThumbnailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? Guidanc { get; set; }
    public string? Catalog { get; set; }
}
