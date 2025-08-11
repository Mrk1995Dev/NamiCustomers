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
    public async Task<ResultDto<GetReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumberAsync(string chassisVinNumber)
    {
        return await dealerService.GetReceptionsInformationByVinNumber(chassisVinNumber);
    }
    [HttpGet("[action]")]
    public async Task<ResultDto<GetBranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId)
    {
        return await dealerService.GetBranchesByDealerAsync(dealerId);
    }

}
