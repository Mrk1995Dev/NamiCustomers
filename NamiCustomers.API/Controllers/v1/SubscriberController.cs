using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Application.Services.Subscribers;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class SubscriberController(
        ISubscriberService subscriberService) : ControllerBase
    {

        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCities()
        {
            var result=await subscriberService.GetCitiesAsync();
                 if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
          

        [HttpGet("[action]")]
        public async Task<IActionResult> InfoByNationalCode([FromQuery] string nationalCode)
        {
            var response = await subscriberService.GetByNationalCodeAsync(nationalCode);
            if (response.Succeeded)
            {
                return Ok(response);
            }

            return NotFound(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Info([FromQuery] int id)
        {
            var response = await subscriberService.GetAsync(id);
            if (response.Succeeded)
            {
                return Ok(response);
            }

            return NotFound(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByMobile([FromQuery] string mobile)
        {
            var result=await subscriberService.GetAsync(mobile);

                if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
         
        [HttpGet("[action]")]
        public async Task<IActionResult> Subscribers()
        {
            var data = await subscriberService.GetAllAsync();
            if (data.Succeeded)
                return Ok(data.Data);

            return NotFound(data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] SubscriberDto customerInfo)
        {
            var result = await subscriberService.RegisterAsync(customerInfo);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Edit([FromBody] SubscriberDto updateCustomer)
        {
            if (!ModelState.IsValid) return BadRequest(Infrastucture.Properties.Resources.errInputInValid);
            var data = await subscriberService.EditAsync(updateCustomer);
            if (data.Succeeded) return Ok(data);

            return BadRequest(data);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var data = await subscriberService.DeleteAsync(id);
            if (data.Succeeded)
                return Ok(data.Data);

            return BadRequest(data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ExportCustomerInfo()
        {
            var data = await subscriberService.ExportAsync();
            if (!data.Succeeded) return NotFound();

            return File(data.Data, "text/palin", "CustomerInfoReport.txt");
        }












    }
}
