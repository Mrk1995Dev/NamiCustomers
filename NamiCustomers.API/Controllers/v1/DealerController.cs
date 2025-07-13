using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Abstractions.Dtos.Dealers;
using NamiCustomers.Application.Services.Dealers;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class DealerController(IDealerService  dealerService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ResultDto<DealerDto>> Get(int id)
        {
            return await dealerService.GetAsync(id);
        }
        [HttpGet("GetAll")]
        public async Task<ResultDto<List<DealerDto>>> GetAll()
        {
            return await dealerService.GetAllAsync();
        }
        [HttpPost]
        public async Task<ResultDto<DealerDto>> Post(DealerDto dealerDto)
        {
            return await dealerService.RegisterAsync(dealerDto);
        }
        [HttpDelete]
        public async Task<ResultDto<DealerDto>> Delete(int id)
        {
            return await dealerService.RemoveAsync(id);
        }
        [HttpPut]
        public async Task<ResultDto<DealerDto>> Put(DealerDto DealerDto)
        {
            return await dealerService.EditAsync(DealerDto);
        }
    }
}
