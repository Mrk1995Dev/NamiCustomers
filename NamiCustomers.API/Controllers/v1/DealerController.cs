using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Application.Services.Dealers;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class DealerController(IDealerService  dealerService) : ControllerBase
{
    [HttpGet("GetDealers")]
    public async Task<ResultDto<DealerResponse[]>> GetDealersAsync()
    {
        return await dealerService.GetDealersAsync();
    }
}
