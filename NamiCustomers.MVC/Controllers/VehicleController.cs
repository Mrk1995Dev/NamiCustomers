using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.MVC.Services;
using System.ComponentModel.DataAnnotations;

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
        var result = await vehicleService.GetAsync(id);

        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }
    public async Task<IActionResult> Default(int id)
    {
        var result = await vehicleService.SetDefaultAsync(id);
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> Index()
    {
        var result = await vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id);
        if (!result.Succeeded)
        {
            SetError(   result.Errors.FirstOrDefault());
        }
        return View(result.Data);
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
        if (string.IsNullOrEmpty(vinNumber))
        {
            SetError( "شماره شاسی را وارد نمایید");
            return RedirectToAction("ChassisInformationByVinNumber");
        }
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
        if (!ModelState.IsValid)
        {
            SetError(Infrastucture.Properties.Resources.errInputInValid);
            return RedirectToAction("Index");
        }
        var result = await vehicleService.RegisterAsync(vehicleModelDto);

        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await vehicleService.GetAsync(id);
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(VehicleModelDto vehicleModelDto)
    {
        if (!ModelState.IsValid)
        {
            SetError(Infrastucture.Properties.Resources.errInputInValid);
            return RedirectToAction("Index");
        }

        var result = await vehicleService.EditAsync(vehicleModelDto);
        if (!result.Succeeded)
        {
            SetError(result.Message);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await vehicleService.RemoveAsync(id);
        if (!result.Succeeded)
        {
            SetError(result.Message);
        }
        return RedirectToAction("Index");
    }
}
