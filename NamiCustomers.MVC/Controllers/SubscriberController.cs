using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services.Subscribers.Dtos;
using NamiCustomers.MVC.Services.Subscribers.Implementation;

namespace NamiCustomers.MVC.Controllers
{

    public class SubscriberController(
        ISubscriberService  subscriberService) : Controller
    {
        
        
        public async Task<IActionResult> Details(int id)
        {
            var data = await subscriberService.GetByIdAsync(id);
            return View(data.Data);  
        }

         
        public async Task<IActionResult> List()
        {
            var data = await subscriberService.GetAllAsync("09191646456");
            return View(data);
        }
 
        public async Task<IActionResult> Register([FromBody] AddSubscriberDto customerInfo)
        {
            var result = await subscriberService.CreateAsync(customerInfo);

            if (result.IsSuccess) return Created();

            return NotFound(result);
        }
 
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
}
