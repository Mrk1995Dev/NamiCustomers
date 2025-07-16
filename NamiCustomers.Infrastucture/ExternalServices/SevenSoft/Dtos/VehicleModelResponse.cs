namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class VehicleModelResponse
{
    public string UniqueId { get; set; }
    public int Code { get; set; }
    public string? VehicleId { get; set; }
    public string? VehicleModelLocalizeName { get; set; }
    public string? VehicleModelEnglishName { get; set; }
    public string? VehicleName { get; set; }
    public string? Description { get; set; }
    public string? VehicleType { get; set; }
}
