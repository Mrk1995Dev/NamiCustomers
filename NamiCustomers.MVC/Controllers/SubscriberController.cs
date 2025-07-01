using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

[Authorize]
public class SubscriberController(
    ISubscriberService  subscriberService) : Controller
{
    
    
    public async Task<IActionResult> Details(int id)
    {
        var data = await subscriberService.GetByIdAsync(id);
        return View(data.Data);  
    }
    public async Task<IActionResult> Profile()
    {
        var data = await subscriberService.GetByNationalCodeAsync();
        return View(data.Data);
    }


    public async Task<IActionResult> List()
    {
        var data = await subscriberService.GetAllAsync();
        return View(data);
    }

    public async Task<IActionResult> Register([FromBody] AddSubscriberDto customerInfo)
    {
        var result = await subscriberService.CreateAsync(customerInfo);

        if (result.IsSuccess) return Created();

        return NotFound(result);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var data = await subscriberService.GetByIdAsync(id);
        return View(data.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] UpdateSubscriberDto updateCustomer)
    {
        if (!ModelState.IsValid) return BadRequest("اطلاعات مربوطه ناقص می باشد.");
        var data = await subscriberService.UpdateAsync(updateCustomer);
        if (data.IsSuccess) return Ok(data);

        return BadRequest(data);
    }

    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var data = await subscriberService.DeleteAsync(id);
        if (data.IsSuccess) return Ok();

        return NotFound(data);
    }

}
