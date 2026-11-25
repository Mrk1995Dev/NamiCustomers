using System.Net;
using System.Net.Http.Json;
using NamiCustomers.Abstractions.Dtos;

namespace NamiCustomers.BackOffice.Services.Booking;

/// <summary>
/// Calls BackOfficeAPI / NamiCustomers.API Reservation.GetBooking (Sevensoft GetBooking).
/// </summary>
public class ReservationBookingService(HttpClient httpClient) : IReservationBookingService
{
    public async Task<ResultDto<BookingResultDto[]>> GetBookingAsync(string bookingCode, string chassisNumber)
    {
        if (string.IsNullOrWhiteSpace(bookingCode) || string.IsNullOrWhiteSpace(chassisNumber))
        {
            return ResultDto.Failure<BookingResultDto[]>(
                "شماره رزرو و شماره شاسی هر دو الزامی هستند.");
        }

        var url =
            $"Reservation/GetBooking?bookingCode={Uri.EscapeDataString(bookingCode.Trim())}" +
            $"&chassisNumber={Uri.EscapeDataString(chassisNumber.Trim())}";

        var response = await httpClient.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new ResultDto<BookingResultDto[]>("", false)
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

        var result = await response.Content.ReadFromJsonAsync<ResultDto<BookingResultDto[]>>();
        return result ?? ResultDto.Failure<BookingResultDto[]>("دریافت وضعیت خودرو با خطا مواجه شد.");
    }
}
