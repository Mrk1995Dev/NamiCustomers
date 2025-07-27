using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

[Authorize(Policy =nameof(MyPloicies.SubscriberAccess))]
public class SubscriberController(
    ISubscriberService  subscriberService) : MyBaseController
{
   [AllowAnonymous]
    public IActionResult CheckMyClaims()
    {
        var a = nameof(MyPloicies.SubscriberAccess);
        return Json(User.Claims.Select(c => new { c.Type, c.Value }));
    }

    public async Task<IActionResult> Details(int id)
    {
        var result = await subscriberService.GetAsync(id);
        if (result.Succeeded)
            return View(result.Data);

        return MyError(result.Errors);
    }
    public async Task<IActionResult> Profile()
    {
        var result = await subscriberService.GetByNationalCodeAsync();
        if (result.Succeeded)
            return View(result.Data);

        return MyError(result.Errors);
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
        var result = await subscriberService.GetAsync(id);
        if (result.Succeeded)
            return View(result.Data);

        return MyError(result.Errors);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(SubscriberDto subscriberDto)
    {
        if (!ModelState.IsValid) return BadRequest("اطلاعات مربوطه ناقص می باشد.");
        var result = await subscriberService.EditAsync(subscriberDto);
        
        if (result.Succeeded)
            return RedirectToAction("Profile");

        return MyError(new List<string> { result.Message});
    }

    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await subscriberService.RemoveAsync(id);
       
        if (result.Succeeded)
            RedirectToAction("List");

        return MyError(new List<string> { result.Message });
    }

}
