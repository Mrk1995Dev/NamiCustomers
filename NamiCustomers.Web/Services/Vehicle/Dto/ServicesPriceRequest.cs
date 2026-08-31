namespace NamiCustomers.Web.Services.Vehicle.Dto
{
    public class ServicesPriceRequest
    {
        public Guid? VehicleModelId { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string ChassisVinNumber { get; set; } = null!;
        public string NationalCodeOrEconomicCode { get; set; } = null!;
        public Guid BranchId { get; set; }
        public Guid UniqueId { get; set; }
    }
}