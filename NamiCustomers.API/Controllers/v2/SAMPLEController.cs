

using NamiCustomers.Application.Services.SevenSoftServices;

namespace NamiCustomers.API.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class SAMPLEController : v1.SAMPLEController
    {
        public SAMPLEController(ISevenSoftService sevenSoftService) : base(sevenSoftService)
        {
        }
    }
}
