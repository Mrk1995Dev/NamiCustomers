 using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Application.Services.Dealers;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class DealerController(IDealerService dealerService) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<ResultDto<DealerResponse[]>> GetDealersAsync()
    {
        return await dealerService.GetDealersAsync();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetReceptionsInformationByVinNumberAsync(string chassisVinNumber)
    {
        var result=await dealerService.GetReceptionsInformationByVinNumber(chassisVinNumber);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBranchesByDealerAsync(Guid dealerId)
    {
        var result= await dealerService.GetBranchesByDealerAsync(dealerId);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

}
