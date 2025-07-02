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
        var data = await subscriberService.GetAsync(id);
        return View(data.Data);  
    }
    public async Task<IActionResult> Profile()
    {
        var data = await subscriberService.GetByNationalCodeAsync();
        return View(data.Data);
    }
 
    public async Task<IActionResult> List()
    {
        var data = await subscriberService.GetAsync();
        return View(data);
    }

    public async Task<IActionResult> Register([FromBody] SubscriberDto subscriberDto)
    {
        var result = await subscriberService.RegisterAsync(subscriberDto);

        if (result.IsSuccess) return Created();

        return NotFound(result);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var data = await subscriberService.GetAsync(id);
        return View(data.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] SubscriberDto subscriberDto)
    {
        if (!ModelState.IsValid) return BadRequest("اطلاعات مربوطه ناقص می باشد.");
        var data = await subscriberService.EditAsync(subscriberDto);
        if (data.IsSuccess) return Ok(data);

        return BadRequest(data);
    }

    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var data = await subscriberService.RemoveAsync(id);
        if (data.IsSuccess) return Ok();

        return NotFound(data);
    }

}
