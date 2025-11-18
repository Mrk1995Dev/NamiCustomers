using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using System.Reflection;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]//todo moradi
public class VehicleController(IVehicleService vehicleService, ISevenSoftService sevenSoftService, IMapper mapper) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await vehicleService.GetAsync(id);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll(int subscriberId)
    {
        var result = await vehicleService.GetAllAsync(subscriberId);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Register(VehicleModelDto vehicleModelDto)
    {
        var result = await vehicleService.RegisterAsync(vehicleModelDto);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await vehicleService.RemoveAsync(id);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> SetDefault(int id)
    {
        var result = await vehicleService.SetDefaultAsync(id);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> Edit(VehicleModelDto vehicleModelDto)
    {
        var result = await vehicleService.EditAsync(vehicleModelDto);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetChassisInformationByVinNumber(string vinNumber)
    {
        if (string.IsNullOrEmpty(vinNumber))
        {
            return BadRequest(ResultDto.Failure<VehicleModelDto>(
          Infrastucture.Properties.Resources.errNotFound));
        }
        var result = await sevenSoftService.GetChassisInformationByVinNumber(vinNumber);
        if (result == null)
            return BadRequest(ResultDto.Failure<VehicleModelDto>(
       Infrastucture.Properties.Resources.errNotFound
       ));

        return Ok(ResultDto.Success<VehicleModelDto>(mapper.Map<VehicleModelDto>(result)));
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetSpecificCases(string vinNumber, string nationalCodeOrEconomicCode, string mobile)
    {
        var result = await sevenSoftService.GetSpecificCases(vinNumber, nationalCodeOrEconomicCode, mobile);
        if (result == null)
            return BadRequest(ResultDto.Failure<string[]>(
       Infrastucture.Properties.Resources.errNotFound));

        return Ok(ResultDto.Success<string[]>(mapper.Map<string[]>(result))
       );

    }
    [HttpGet("[action]")]
    public async Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber)
    {
        var result = await sevenSoftService.GetActiveMainChassisGuarantee(vinNumber);
        if (result == null) 
            return ResultDto.Failure<ActiveMainChassisGuaranteeResponse>(
        Infrastucture.Properties.Resources.errNotFound
);
        return ResultDto.Success<ActiveMainChassisGuaranteeResponse>(
      mapper.Map<ActiveMainChassisGuaranteeResponse>(result)
       );
    }

    [HttpGet("[action]")]
    public async Task<ResultDto<PartsPriceByChassisResponse[]>> GetPartsPriceByChassis(PartsPriceByChassisRequest getPartsPriceByChassisRequest)
    {
        var result = await sevenSoftService.GetPartsPriceByChassis(getPartsPriceByChassisRequest);
        return result;
    }
    [HttpGet("[action]")]
    public async Task<SpOrderingsBySubscriberResponse[]> GetSpOrderingsBySubscriber(string chassisVinNumber, string nationalCodeOrEconomicCode)
    {
        var result = await sevenSoftService.GetSpOrderingsBySubscriber(chassisVinNumber,nationalCodeOrEconomicCode);
        return result;
    }
    [HttpGet("[action]")]
    public async Task<SpOrderingPartSpOrderingCodeResponse[]> GetSpOrderingPartSpOrderingCode(string spOrderingCode)
    {
        var result = await sevenSoftService.GetSpOrderingPartSpOrderingCode(spOrderingCode);
        return result;
    }
    [HttpGet("[action]")]
    public async Task<AllOrderStatusTypeResponse[]> GetAllOrderStatusType()
    {
        var result = await sevenSoftService.GetAllOrderStatusType();
        return result;
    }
}
