using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Application.Services.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using System.Security.Claims;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]//todo moradi
public class VehicleController(IVehicleService vehicleService,
    ISevenSoftService sevenSoftService,
    IMapper mapper,
    ISubscriberService subscriberService) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var appUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await vehicleService.GetAsync(id);
        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var appUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await vehicleService.GetAllAsync(appUserId);
        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(VehicleModelDto vehicleModelDto)
    {
        if (vehicleModelDto is null || string.IsNullOrWhiteSpace(vehicleModelDto.VinNumber))
        {
            return BadRequest(ResultDto.Failure<VehicleModelDto>("شماره شاسی را وارد کنید."));
        }

        vehicleModelDto.VinNumber = vehicleModelDto.VinNumber.Trim().ToUpperInvariant();

        if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var subscriberId))
            vehicleModelDto.SubscriberId = subscriberId;

        if (string.IsNullOrWhiteSpace(vehicleModelDto.NationalCode))
            vehicleModelDto.NationalCode = User.FindFirst("NationalCode")?.Value ?? string.Empty;

        if (string.IsNullOrWhiteSpace(vehicleModelDto.Mobile))
            vehicleModelDto.Mobile = User.FindFirst("Mobile")?.Value ?? string.Empty;

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
    public async Task<IActionResult> GetSpecificCases([FromQuery] string vinNumber)
    {
        var nationalCodeOrEconomicCode = User.FindFirst("NationalCode")?.Value;
        var mobile = User.FindFirst("Mobile")?.Value;
        var result = await sevenSoftService.GetSpecificCases(vinNumber, nationalCodeOrEconomicCode, mobile);

       // if (result == null)
       //     return BadRequest(ResultDto.Failure<string[]>(
       //Infrastucture.Properties.Resources.errNotFound));

        return Ok(ResultDto.Success<string[]>(mapper.Map<string[]>(result))
       );

    }

    [HttpGet("[action]")]
    public async Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee([FromQuery] string vinNumber)
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
    public async Task<ResultDto<PartsPriceByChassisResponse[]>> GetPartsPriceByChassis([FromQuery]PartsPriceByChassisRequest getPartsPriceByChassisRequest)
    {
        var result = await sevenSoftService.GetPartsPriceByChassis(getPartsPriceByChassisRequest);
        return result;
    }
    [HttpGet("[action]")]
    public async Task<SpOrderingsBySubscriberResponse[]> GetSpOrderingsBySubscriber(
        [FromQuery] string chassisVinNumber,
        [FromQuery] string nationalCodeOrEconomicCode)
    {
        var result = await sevenSoftService.GetSpOrderingsBySubscriber(chassisVinNumber, nationalCodeOrEconomicCode);
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

    [HttpPost("[action]")]
    public async Task<IActionResult> ServicesPriceList([FromBody] ServicesPriceRequest request)
    {
        var nationalCode = User.FindFirst("NationalCode")?.Value;
        if (string.IsNullOrEmpty(request.ServiceCode) && string.IsNullOrEmpty(request.ServiceName))
        {
            request.DealerId = Guid.Empty;
            request.BranchId = Guid.Empty;
            var result = ResultDto.Failure(Infrastucture.Properties.Resources.errRequiredServiceCodeName);
            return BadRequest(result);
        }

        var subscriber = (await subscriberService.GetByNationalCodeAsync(nationalCode)).Data;
        var vinNumber = subscriber.VehicleModels?.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        if (vinNumber != null)
        {
            request.NationalCodeOrEconomicCode = subscriber.NationalCode;
            request.ChassisVinNumber = vinNumber;
            var data = await sevenSoftService.GetServicesPriceByBranchId(request);
            if (!data.Succeeded)
            {
                var result = ResultDto.Failure(data.Message);
                return BadRequest(result);
            }
            return Ok(data);
        }
        else
        {
            return BadRequest(ResultDto.Failure(Infrastucture.Properties.Resources.errRequiredServiceCodeName));
        }
    }
}