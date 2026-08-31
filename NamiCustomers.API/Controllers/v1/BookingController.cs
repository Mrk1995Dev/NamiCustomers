using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using NamiCustomers.Application.Services.Book.Contract;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class BookingController(IBookingService bookingService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBook([FromQuery] string vinNumber)
        {
            return Ok(await bookingService.GetBookingAsync(vinNumber));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CheckExistsReserveVinNumber([FromQuery] string vinNumber)
        {
            return Ok(await bookingService.CheckExistsReserveVinNumberAsync(vinNumber));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CheckExistsReservePhoneNumber([FromQuery] string phoneNumber)
        {
            return Ok(await bookingService.CheckExistsReservePhoneNumberAsync(phoneNumber));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CancelBooking([FromQuery] Guid bookId , [FromQuery] string number)
        {
            return Ok(await bookingService.CancelBookingAsync(bookId, number));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertBooking(InsertBookingRequest request)
        {
            return Ok(await bookingService.InsertBookingAsync(request));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CheckIsValidKilometer([FromQuery]string vinNumber, [FromQuery]int kilometer)
        {
            return Ok(await bookingService.CheckIsValidKilometerAsync(vinNumber, kilometer));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCity([FromQuery] Guid subCountryId)
        {
            return Ok(await bookingService.GetAllCityAsync(subCountryId));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDealer(Guid cityId)
        {
            return Ok(await bookingService.GetAllDealerAsync(cityId));
        }
    }
}
