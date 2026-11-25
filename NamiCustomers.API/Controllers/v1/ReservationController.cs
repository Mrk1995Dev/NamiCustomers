using Microsoft.AspNetCore.Authorization;
using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Application.Services.Book.Contract;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.API.Controllers.v1;

/// <summary>
/// BackOffice-equivalent reservation/vehicle-status endpoints
/// (maps from local NamiCustomers.BackOfficeAPI ReservationController).
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class ReservationController(IBookingService bookingService) : ControllerBase
{
    /// <summary>
    /// جستجوی وضعیت خودرو / نوبت با شماره نوبت‌دهی و شماره شاسی — فراخوانی Sevensoft GetBooking.
    /// </summary>
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBooking(
        [FromQuery] string bookingCode,
        [FromQuery] string chassisNumber)
    {
        if (string.IsNullOrWhiteSpace(bookingCode) || string.IsNullOrWhiteSpace(chassisNumber))
        {
            return BadRequest(ResultDto.Failure<BookingResponse[]>(
                "شماره نوبت‌دهی و شماره شاسی هر دو الزامی هستند."));
        }

        return Ok(await bookingService.GetBookingByCodeAndChassisAsync(bookingCode, chassisNumber));
    }
}
