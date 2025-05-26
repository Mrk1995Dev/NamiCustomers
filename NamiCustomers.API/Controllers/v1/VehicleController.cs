using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Application.Services.Vehicles;
using NamiCustomers.Application.Services.Vehicles.Dtos;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class VehicleController(IVehicleService vehicleService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ResultDto<VehicleModelDto>> Get(int id)
        {
            return await vehicleService.GetAsync(id);
        }
        [HttpGet("GetAll")]
        public async Task<ResultDto<List<VehicleModelDto>>> GetAll()
        {
            return await vehicleService.GetAllAsync();
        }
        [HttpPost]
        public async Task<ResultDto<VehicleModelDto>> Post(VehicleRegisterDto vehicleRegisterDto)
        {
            return await vehicleService.RegisterByVinNumberAsync(vehicleRegisterDto);
        }
        [HttpDelete]
        public async Task<ResultDto<VehicleModelDto>> Delete(int id)
        {
            return await vehicleService.RemoveAsync(id);
        }
        [HttpPut]
        public async Task<ResultDto<VehicleModelDto>> Put(VehicleModelDto vehicleModelDto)
        {
            return await vehicleService.EditAsync(vehicleModelDto);
        }
    }
}
