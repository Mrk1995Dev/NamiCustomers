using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Application.Services.Subscribers;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CityController(ISubscriberService customerManagmentService) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<List<CityDto>> GetAllCities()
            => await customerManagmentService.GetAllCitiesAsync();
    }
}
