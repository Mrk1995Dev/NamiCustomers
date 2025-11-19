 
using Microsoft.AspNetCore.Authorization;

using NamiCustomers.Abstractions.Dtos.Appointments;
using NamiCustomers.Application.Services.Appointments;
using NamiCustomers.Application.Services.Dealers;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class AppointmentController(IAppointmentService appointmentService, IDealerService dealerService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result=  await appointmentService.GetAsync(id);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result= await appointmentService.GetAllAsync();
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPost]
    public async Task<IActionResult> Post(DefineAppointmentDto defineAppointmentDto)
    {
        var result= await appointmentService.DefineAppointments(defineAppointmentDto);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result= await appointmentService.RemoveAsync(id);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPut]
    public async Task<IActionResult> Put(AppointmentDto AppointmentDto)
    {
        var result= await appointmentService.EditAsync(AppointmentDto);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
