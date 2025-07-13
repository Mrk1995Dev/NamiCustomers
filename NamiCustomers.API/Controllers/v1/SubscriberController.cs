using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Application.Services.Subscribers;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SubscriberController(
        ISubscriberService subscriberService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ResultDto<SubscriberDto>> Info([FromQuery] int id)
            => await subscriberService.GetAsync(id);
        [HttpGet("[action]")]
        public async Task<ResultDto<SubscriberDto>> InfoByNationalCode([FromQuery] string nationalCode)
            => await subscriberService.GetByNationalCodeAsync(nationalCode);

        

        [HttpGet("[action]")]
        public async Task<ResultDto<SubscriberDto>> GetByMobile([FromQuery] string mobile)
           => await subscriberService.GetAsync(mobile);


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

            if (result.Succeeded) return Created();

            return NotFound(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromBody] SubscriberDto updateCustomer)
        {
            if (!ModelState.IsValid) return BadRequest("اطلاعات مربوطه ناقص می باشد.");
            var data = await subscriberService.EditAsync(updateCustomer);
            if (data.Succeeded) return Ok(data);

            return BadRequest(data);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var data = await subscriberService.DeleteAsync(id);
            if (data.Succeeded) return Ok();

            return NotFound(data);
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
