using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

[Authorize(Policy = "SubscriberAccess")]
//[Authorize(Roles = "Subscriber,Admin")]
public class SubscriberController(
    ISubscriberService  subscriberService) : Controller
{
   [AllowAnonymous]
    public IActionResult CheckMyClaims()
    {
        return Json(User.Claims.Select(c => new { c.Type, c.Value }));
    }

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

        if (result.Succeeded) return Created();

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
        if (data.Succeeded) return Ok(data);

        return BadRequest(data);
    }

    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var data = await subscriberService.RemoveAsync(id);
        if (data.Succeeded) return Ok();

        return NotFound(data);
    }

}
