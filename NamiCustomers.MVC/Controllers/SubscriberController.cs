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
        if (!result.Succeeded)
        {
            SetError(result.Errors);
        }
        return View(result.Data);
    }
    public async Task<IActionResult> Profile()
    {
        var result = await subscriberService.GetByNationalCodeAsync();
        if (!result.Succeeded)
        {
            SetError(result.Errors);
        }
        return View(result.Data);
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
        if (!result.Succeeded)
        {
            SetError(result.Errors);
        }
        return View(result.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(SubscriberDto subscriberDto)
    {
        if (!ModelState.IsValid) { 
            SetError(new List<string> { "اطلاعات مربوطه ناقص می باشد." });
            return RedirectToAction("Profile");
        }
        var result = await subscriberService.EditAsync(subscriberDto);

        if (!result.Succeeded)
        {
            SetError(new List<string> { result.Message });
        }
        return RedirectToAction("Profile");
    }

    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await subscriberService.RemoveAsync(id);
       
        if (!result.Succeeded)
        {
            SetError(new List<string> { result.Message });
        }
        return RedirectToAction("List");
    }

}
