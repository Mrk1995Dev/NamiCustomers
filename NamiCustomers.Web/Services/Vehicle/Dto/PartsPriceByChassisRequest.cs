namespace NamiCustomers.Web.Services.Vehicle.Dto
{
    public class PartsPriceByChassisRequest
    {
        public string VehicleModelId { get; set; }
        public string? PartNo { get; set; }
        public string ChassisVinNumber { get; set; }
        public string NationalCodeOrEconomicCode { get; set; }
        public string? PartName { get; set; }
        public object PartSupplierNo { get; set; }
    }
}