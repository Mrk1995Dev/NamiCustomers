using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.MVC.Services;
using System.Numerics;

namespace NamiCustomers.MVC.Controllers;

[Authorize(Policy = nameof(MyPloicies.SubscriberAccess))]
public class VehicleController(
    ISubscriberService subscriberService, IVehicleService vehicleService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> ActiveMainChassisGuarantee(string? VinNumber)
    {
        if (VinNumber != null)
        {
            var data = await vehicleService.GetActiveMainChassisGuarantee(VinNumber);
            if (!data.Succeeded)
            {
                return View(new ActiveMainChassisGuaranteeResponse() { VinNumber = VinNumber });
            }
            data.Data.VinNumber = VinNumber;
            return View(data.Data);
        }
        return View(new ActiveMainChassisGuaranteeResponse() { VinNumber = VinNumber });
    }


    public async Task<IActionResult> Details(int id)
    {
        var data = await vehicleService.GetAsync(id);
        return View(data.Data);
    }

    public async Task<IActionResult> Index()
    {
        var data = await vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id);
        return View(data.Data);
    }
    [HttpGet]
    public async Task<IActionResult> Create(VehicleModelDto? vehicleModelDto)
    {
        if (vehicleModelDto is null)
        {
            vehicleModelDto = new VehicleModelDto { SubscriberId = subscriberService.CurrentSubscriber.Id };
        }

        return View(vehicleModelDto);
    }


    [HttpGet]
    public async Task<IActionResult> ChassisInformationByVinNumber()
    {
        return View(new VehicleModelDto { SubscriberId = subscriberService.CurrentSubscriber.Id, VinNumber = "LGBH9VEAXPY770511" });
    }

    [HttpGet]
    public async Task<IActionResult> GetChassisInformationByVinNumber(string vinNumber)
    {
        var data = await vehicleService.GetChassisInformationByVinNumber(vinNumber);
        if (data != null)
        {
            return RedirectToAction("Create", data.Data);
        }
        return View(new VehicleModelDto { SubscriberId = subscriberService.CurrentSubscriber.Id });
    }



    [HttpPost]
    public async Task<IActionResult> CreateVehicle(VehicleModelDto vehicleModelDto)
    {
        if (!ModelState.IsValid) return BadRequest(Infrastucture.Properties.Resources.errInputInValid);
        var result = await vehicleService.RegisterAsync(vehicleModelDto);

        if (result.Succeeded)
            return RedirectToAction("Index");

        return NotFound(result);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var data = await vehicleService.GetAsync(id);
        return View(data.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(VehicleModelDto vehicleModelDto)
    {
        if (!ModelState.IsValid) return BadRequest(Infrastucture.Properties.Resources.errInputInValid);
        var data = await vehicleService.EditAsync(vehicleModelDto);
        if (data.Succeeded) return RedirectToAction("Index");

        return BadRequest(data);
    }

    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var data = await vehicleService.RemoveAsync(id);
        if (data.Succeeded) return RedirectToAction("Index");

        return NotFound(data);
    }

}
