using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NamiCustomers.Abstractions.Dtos.Vehicles
{
    public class VehicleModelDto
    {
        public int Id              { get; set; }
        public int SubscriberId { get; set; }
        [Required]
        public string VehicleName { get; set; }
        public string? EnglishName { get; set; }
        public string? Description { get; set; }
        public string? ProductYear { get; set; }
        public string? BodyColor { get; set; }
        public string? MotorNumber { get; set;   }
        public string? FullSystem { get;set; }
        public Guid? VehicleModelId { get; set; }
        public string? ChassisUsageTypeName { get; set; }

		public string? SelectedVehicleCommonName { get;set; }
        public string? SelectedVehicleDescription { get;set; }
		/// <summary>
		/// شماره شاسی 
		/// </summary>
		[Required]
        public string VinNumber { get; set; }
        public Guid? SalePlanIdSevenSoft { get; set; }
        public Guid? SaleBasketIdSevenSoft { get; set; }
        public Guid? VehicleModelIdSevensoft { get; set; }
        public Guid? BrandIdSevenSoft { get; set; }
    }
}