using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Application.Services.Subscribers;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    //[Authorize]
    public class SubscriberController(
        ISubscriberService customerManagmentService) : ControllerBase
    {
        [HttpGet("info")]
        public async Task<ResultDto<SubscriberDto>> CustomerInfo([FromQuery] int id)
            => await customerManagmentService.GetAsync(id);
        [HttpGet("infobynationalcode")]
        public async Task<ResultDto<SubscriberDto>> InfoByNationalCode([FromQuery] string nationalCode)
            => await customerManagmentService.GetByNationalCodeAsync(nationalCode);

        

        [HttpGet("GetByMobile")]
        public async Task<ResultDto<SubscriberDto>> CustomerMobile([FromQuery] string mobile)
           => await customerManagmentService.GetAsync(mobile);


        [HttpGet("subscribers")]
        public async Task<IActionResult> GetListCustomerInfo()
        {
            var data = await customerManagmentService.GetAllAsync();
            if (data.IsSuccess)
                return Ok(data.Data);

            return NotFound(data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddCustomerInfo([FromBody] SubscriberDto customerInfo)
        {
            var result = await customerManagmentService.RegisterAsync(customerInfo);

            if (result.IsSuccess) return Created();

            return NotFound(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateCustomer([FromBody] SubscriberDto updateCustomer)
        {
            if (!ModelState.IsValid) return BadRequest("اطلاعات مربوطه ناقص می باشد.");
            var data = await customerManagmentService.EditAsync(updateCustomer);
            if (data.IsSuccess) return Ok(data);

            return BadRequest(data);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] int id)
        {
            var data = await customerManagmentService.DeleteAsync(id);
            if (data.IsSuccess) return Ok();

            return NotFound(data);
        }

        [HttpGet("report")]
        public async Task<IActionResult> ExportCustomerInfo()
        {
            var data = await customerManagmentService.ExportAsync();
            if (!data.IsSuccess) return NotFound();

            return File(data.Data, "text/palin", "CustomerInfoReport.txt");
        }
    }
}
