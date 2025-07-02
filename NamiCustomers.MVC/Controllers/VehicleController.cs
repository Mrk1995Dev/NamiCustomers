using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

[Authorize]
public class VehicleController(
    ISubscriberService  subscriberService,IVehicleService vehicleService) : Controller
{

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
    public async Task<IActionResult> Create()
    {
        return View(new VehicleModelDto { SubscriberId=subscriberService.CurrentSubscriber.Id });
    }
    [HttpPost]
    public async Task<IActionResult> Create(VehicleModelDto  vehicleModelDto)
    {
        if (!ModelState.IsValid) return BadRequest(Infrastucture.Properties.Resources.errInputInValid);
        var result = await vehicleService.RegisterAsync(vehicleModelDto);

        if (result.IsSuccess) return  RedirectToAction("Index");

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
        if (data.IsSuccess) return RedirectToAction("Index");

        return BadRequest(data);
    }
 
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var data = await vehicleService.RemoveAsync(id);
        if (data.IsSuccess) return RedirectToAction("Index");

        return NotFound(data);
    }

}
