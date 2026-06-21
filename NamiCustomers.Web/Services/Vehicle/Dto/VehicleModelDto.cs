using NamiCustomers.Abstractions.Dtos.Vehicles;

namespace NamiCustomers.Web.Services.Vehicle.Dto
{
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

        /// <summary>
        /// شماره شاسی 
        /// </summary>
        public string? VinNumber { get; set; }
        //public Guid? SalePlanIdSevenSoft { get; set; }
        //public Guid? SaleBasketIdSevenSoft { get; set; }
        //public Guid? BrandIdSevenSoft { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public VehicleAttachmentDto? VehicleAttachment { get; set; }
    }
}