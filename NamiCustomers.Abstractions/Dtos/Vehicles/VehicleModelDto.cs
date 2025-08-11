using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Vehicles;

public class VehicleModelDto
{
    public int? Id { get; set; }
    public int? SubscriberId { get; set; }
    [Display(Name = "مدل خودرو ")]
    public string? VehicleModelName { get; set; }
    [Display(Name = " نام خودرو")]
    public string? VehicleModelLocalizedName { get; set; }
    [Display(Name = "نام خودرو")]
    public string? SelectedVehicleDescription { get; set; }
    [Display(Name = "سال ساخت")]
    public string? ProductYear { get; set; }
    [Display(Name = "رنگ بدنه")]
    public string? BodyColor { get; set; }
    [Display(Name = "شماره موتور")]
    public string? MotorNumber { get; set; }
    [Display(Name = "نوع سوخت")]
    public string? FullSystem { get; set; }
    public Guid? VehicleModelIdSevenSoft { get; set; }
    public string? ChassisUsageTypeName { get; set; }
    public bool IsDefault { get; set; }

    public string? SelectedVehicleCommonName { get; set; }

    /// <summary>
    /// شماره شاسی 
    /// </summary>

    [Display(Name = "شماره شاسی")]
    public string? VinNumber { get; set; }
    //public Guid? SalePlanIdSevenSoft { get; set; }
    //public Guid? SaleBasketIdSevenSoft { get; set; }
    //public Guid? BrandIdSevenSoft { get; set; }

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