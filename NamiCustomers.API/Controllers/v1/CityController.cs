using NamiCustomers.Application.Services.Customers;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CityController(ICustomerManagmentService customerManagmentService) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<List<CityDto>> GetAllCities()
            => await customerManagmentService.GetAllCitiesAsync();
    }
}
