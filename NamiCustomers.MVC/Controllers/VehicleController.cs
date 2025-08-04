using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

[Authorize(Policy = nameof(MyPloicies.SubscriberAccess))]
public class VehicleController(
    ISubscriberService subscriberService, IVehicleService vehicleService) : MyBaseController
{
    [HttpGet]
    public async Task<IActionResult> ActiveMainChassisGuarantee(string? VinNumber)
    {

        var relatedVins = await vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id);
        ViewBag.relatedVins = relatedVins.Data.Select(c => new KeyValuePair<string, string>(c.VinNumber, c.VinNumber)).ToList();
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
        return View(new ActiveMainChassisGuaranteeResponse());
    }


    public async Task<IActionResult> Details(int id)
    {
        var data = await vehicleService.GetAsync(id);
        if (data.Succeeded)
        {
            return View(data.Data);
        }
        return MyError(data.Errors);
    }
    public async Task<IActionResult> Default(int id)
    {
        var data = await vehicleService.SetDefaultAsync(id);
        if (data.Succeeded)
        {
            return RedirectToAction("Index");
        }
        return MyError(data.Errors);
    }


    public async Task<IActionResult> Index()
    {
        var data = await vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id);
        if (data.Succeeded)
        {
            return View(data.Data);
        }
        return MyError(data.Errors);
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
        return View(new VehicleModelDto { SubscriberId = subscriberService.CurrentSubscriber.Id, VinNumber = subscriberService.CurrentSubscriber.VehicleModels?.FirstOrDefault(c=>c.IsDefault)?.VinNumber });
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

        return MyError(result.Errors);

    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var data = await vehicleService.GetAsync(id);
        if (data.Succeeded)
        {
            return View(data.Data);
        }
        return MyError(data.Errors);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(VehicleModelDto vehicleModelDto)
    {
        if (!ModelState.IsValid) return BadRequest(Infrastucture.Properties.Resources.errInputInValid);
        var data = await vehicleService.EditAsync(vehicleModelDto);

        if (data.Succeeded)
        {
            return RedirectToAction("Index");
        }
        return MyError(new List<string> { data.Message });

    }

    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var data = await vehicleService.RemoveAsync(id);
        if (data.Succeeded)
        {
            return RedirectToAction("Index");
        }
        return MyError(new List<string> { data.Message });
    }
}
