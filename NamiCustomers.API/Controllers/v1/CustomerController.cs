using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Application.Services.Subscribers.Dtos;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    [NonController]
    public class CustomerController(
        ISubscriberService customerManagmentService) : ControllerBase
    {
        [HttpGet("info")]
        public async Task<ResultDto<SubscriberDetailsDto>> CustomerInfo([FromQuery] int id)
            => await customerManagmentService.GetCustomerInfoDetailAsync(id);

        [HttpGet("customerList")]
        public async Task<IActionResult> GetListCustomerInfo()
        {
            var data = await customerManagmentService.GetCustomerListAsync();
            if (data.Issuccess)
                return Ok(data.Data);

            return NotFound(data);
        }

        [HttpPost("addCustomer")]
        public async Task<IActionResult> AddCustomerInfo([FromBody] AddSubscriberDto customerInfo)
        {
            var result = await customerManagmentService.AddCustomerInfoAsync(customerInfo);

            if (result.IsSuccess) return Created();

            return NotFound(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateSubscriberDto updateCustomer)
        {
            if (!ModelState.IsValid) return BadRequest("اطلاعات مربوطه ناقص می باشد.");
            var data = await customerManagmentService.UpdateCustomerInfo(updateCustomer);
            if (data.IsSuccess) return Ok(data);

            return BadRequest(data);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] int id)
        {
            var data = await customerManagmentService.DeleteCustomerInfoAsync(id);
            if (data.IsSuccess) return Ok();

            return NotFound(data);
        }

        [HttpGet("report")]
        public async Task<IActionResult> ExportCustomerInfo()
        {
            var data = await customerManagmentService.ExportCustomerInfoAsync();
            if (!data.Issuccess) return NotFound();

            return File(data.Data, "text/palin", "CustomerInfoReport.txt");
        }
    }


 
}
