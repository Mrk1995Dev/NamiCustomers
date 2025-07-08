using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NamiCustomers.Abstractions.Dtos.Vehicles;

public class VehicleModelDto
{
    public int? Id              { get; set; }
    public int SubscriberId { get; set; }
    public string? VehicleModelName { get; set; }
    public string? VehicleModelLocalizedName { get; set; }
    public string? SelectedVehicleDescription { get; set; }
    public string? ProductYear { get; set; }
    public string? BodyColor { get; set; }
    public string? MotorNumber { get; set;   }
    public string? FullSystem { get;set; }
    public Guid? VehicleModelId { get; set; }
    public string? ChassisUsageTypeName { get; set; }

		public string? SelectedVehicleCommonName { get;set; }
 
		/// <summary>
		/// شماره شاسی 
		/// </summary>

    public string? VinNumber { get; set; }
    public Guid? SalePlanIdSevenSoft { get; set; }
    public Guid? SaleBasketIdSevenSoft { get; set; }
    public Guid? VehicleModelIdSevensoft { get; set; }
    public Guid? BrandId { get; set; }
}