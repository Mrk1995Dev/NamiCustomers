using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.MVC.Models;

namespace NamiCustomers.MVC.Controllers;

public class ReservationController(ISevenSoftService sevenSoftService) : MyBaseController
{
    [HttpGet]
    public IActionResult VehicleStatus()
    {
        return View(new VehicleStatusSearchViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VehicleStatus(VehicleStatusSearchViewModel model)
    {
        model.BookingCode = model.BookingCode?.Trim() ?? string.Empty;
        model.ChassisNumber = model.ChassisNumber?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(model.BookingCode))
            ModelState.AddModelError(nameof(model.BookingCode), "شماره نوبت‌دهی الزامی است.");

        if (string.IsNullOrWhiteSpace(model.ChassisNumber))
            ModelState.AddModelError(nameof(model.ChassisNumber), "شماره شاسی الزامی است.");

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await sevenSoftService.GetBooking(model.ChassisNumber, model.BookingCode);
        model.Searched = true;

        if (!result.Succeeded || result.Data is null || string.IsNullOrWhiteSpace(result.Data.UniqueId))
        {
            model.ErrorMessage = string.IsNullOrWhiteSpace(result.Message)
                ? "نوبتی با این مشخصات یافت نشد."
                : result.Message;
            model.Results = Array.Empty<BookingResponse>();
            SetError(model.ErrorMessage);
            return View(model);
        }

        model.Results = new[] { result.Data };
        return View(model);
    }
}
