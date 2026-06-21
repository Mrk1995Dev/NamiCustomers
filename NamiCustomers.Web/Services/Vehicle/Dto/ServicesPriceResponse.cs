namespace NamiCustomers.Web.Services.Vehicle.Dto
{
    public class ServicesPriceResponse
    {
        public int RowNumber { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public float Price { get; set; }
        public Guid BranchId { get; set; }
        public float PriceCustomer { get; set; }
        public float PriceWarranty { get; set; }
    }
}