using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.Vehicles;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class VehicleController(IVehicleService vehicleService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ResultDto<VehicleModelDto>> Get(int id)
        {
            return await vehicleService.GetAsync(id);
        }
        [HttpGet("[action]")]
        public async Task<ResultDto<List<VehicleModelDto>>> GetAll(int subscriberId)
        {
            return await vehicleService.GetAllAsync(subscriberId);
        }
        [HttpPost("[action]")]
        public async Task<ResultDto<VehicleModelDto>> Register(VehicleModelDto vehicleModelDto)
        {
            return await vehicleService.RegisterAsync(vehicleModelDto);
        }
        [HttpDelete("[action]")]
        public async Task<ResultDto<VehicleModelDto>> Remove(int id)
        {
            return await vehicleService.RemoveAsync(id);
        }
        [HttpPut("[action]")]
        public async Task<ResultDto<VehicleModelDto>> Edit(VehicleModelDto vehicleModelDto)
        {
            return await vehicleService.EditAsync(vehicleModelDto);
        }
    }
}
