using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class VehicleController(IVehicleService vehicleService, ISevenSoftService sevenSoftService, IMapper mapper) : ControllerBase
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

        [HttpGet("[action]")]
        public async Task<ResultDto<VehicleModelDto>> GetChassisInformationByVinNumber(string vinNumber)
        {
            var result = await sevenSoftService.GetChassisInformationByVinNumber(vinNumber);
            if (result == null) return new ResultDto<VehicleModelDto>(
           Infrastucture.Properties.Resources.errNotFound,
            false,
            null);


            return new ResultDto<VehicleModelDto>(
          Infrastucture.Properties.Resources.msgFound,
          true,
          mapper.Map<VehicleModelDto>(result)
           );

        }

        [HttpGet("[action]")]
        public async Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber)
        {
            var result = await sevenSoftService.GetActiveMainChassisGuarantee(vinNumber);
            if (result == null) return new ResultDto<ActiveMainChassisGuaranteeResponse>(
            Infrastucture.Properties.Resources.errNotFound,
            false,
            null);


            return new ResultDto<ActiveMainChassisGuaranteeResponse>(
          Infrastucture.Properties.Resources.msgFound,
          true,
          mapper.Map<ActiveMainChassisGuaranteeResponse>(result)
           );

        }



        //LGBH9VEAXPY770511
    }
}
