namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [NonController]
    public class SAMPLEController(ISAMPLEService SAMPLEService) : ControllerBase
    {
        private readonly ISAMPLEService _SAMPLEService = SAMPLEService;

        [HttpGet]
        [Route("[action]")]
        public virtual string Alive() => ".Net Core Web API Version 1";

        [HttpPost]
        public virtual async Task<IActionResult> Post(SAMPLEDTO SAMPLEDTO)
        {
            var result = await _SAMPLEService.CreateAsync(SAMPLEDTO);
            return Ok(result);
        }
        [HttpPut]
        [Route("[action]")]
        public virtual async Task<SAMPLEDTO> Update(SAMPLEDTO SAMPLEDTO)
        {
            return await _SAMPLEService.UpdateAsync(SAMPLEDTO);
        }
        [HttpDelete]
        [Route("[action]")]
        public virtual async Task<int> Delete(int id)
        {
            return await _SAMPLEService.DeleteAsync(id);
        }
        [HttpGet]
        [Route("GetAll")]
        public virtual async Task<IActionResult> GetRangeAsync([FromQuery] SAMPLEFilterDTO filter)
        {
            var result = await _SAMPLEService.GetAll(filter);
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _SAMPLEService.GetAsync(id);
            return Ok(result);
        }
    }
}
