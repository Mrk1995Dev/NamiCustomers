

namespace NamiCustomers.API.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class SAMPLEController(ISAMPLEService SAMPLEService) : v1.SAMPLEController(SAMPLEService)
    {
        [HttpGet]
        [Route("[action]")]
        public override string Alive() => ".Net Core Web API Version 2";
    }
}
