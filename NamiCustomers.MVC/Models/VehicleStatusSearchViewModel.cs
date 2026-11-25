using System.ComponentModel.DataAnnotations;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.MVC.Models;

public class VehicleStatusSearchViewModel
{
    [Required(ErrorMessage = "شماره نوبت‌دهی الزامی است.")]
    [Display(Name = "شماره نوبت‌دهی")]
    public string BookingCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "شماره شاسی الزامی است.")]
    [Display(Name = "شماره شاسی")]
    public string ChassisNumber { get; set; } = string.Empty;

    public bool Searched { get; set; }

    public string? ErrorMessage { get; set; }

    public BookingResponse[] Results { get; set; } = Array.Empty<BookingResponse>();
}
