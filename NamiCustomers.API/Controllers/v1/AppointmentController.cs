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
    public async Task<ResultDto<AppointmentDto>> Get(int id)
    {
        return await appointmentService.GetAsync(id);
    }
    [HttpGet("GetAll")]
    public async Task<ResultDto<List<AppointmentDto>>> GetAll()
    {
        return await appointmentService.GetAllAsync();
    }
    [HttpPost]
    public async Task<ResultDto<List<AppointmentDto>>> Post(DefineAppointmentDto defineAppointmentDto)
    {
        return await appointmentService.DefineAppointments(defineAppointmentDto);
    }
    [HttpDelete]
    public async Task<ResultDto<AppointmentDto>> Delete(int id)
    {
        return await appointmentService.RemoveAsync(id);
    }
    [HttpPut]
    public async Task<ResultDto<AppointmentDto>> Put(AppointmentDto AppointmentDto)
    {
        return await appointmentService.EditAsync(AppointmentDto);
    }
}
