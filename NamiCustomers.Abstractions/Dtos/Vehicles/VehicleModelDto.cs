namespace NamiCustomers.Abstractions.Dtos.Vehicles
{
    public class VehicleModelDto
    {
        public int Id { get; set; }
        public int SubscriberId { get; set; }
        public string? VehicleName { get; set; }
        public string? EnglishName { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// شماره شاسی 
        /// </summary>
        public string? VinNumber { get; set; }
        public Guid? SalePlanIdSevenSoft { get; set; }
        public Guid? SaleBasketIdSevenSoft { get; set; }
        public Guid? VehicleModelIdSevensoft { get; set; }
        public Guid? BrandIdSevenSoft { get; set; }
    }
}