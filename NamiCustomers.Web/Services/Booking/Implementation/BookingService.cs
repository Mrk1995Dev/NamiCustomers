using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Web.Services.Booking.Contract;
using NamiCustomers.Web.Services.Booking.Dto;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.Booking.Implementation;

public class BookingService(HttpClient httpClient) : IBookingService
{
    public async Task<ResultDto<BookingDto>> GetBookByVinNumberAsync(string vinNumber)
    {
        var response = await httpClient.GetAsync($"Booking/GetBookByVinNumber?vinNumber={Uri.EscapeDataString(vinNumber)}");

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new ResultDto<BookingDto>("", false)
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

        var result = await response.Content.ReadFromJsonAsync<ResultDto<BookingDto>>();
        return result ?? new ResultDto<BookingDto>("دریافت نوبت با خطا مواجه شد.", false);
    }

    public async Task<ResultDto<BookingDto[]>> GetBookingByCodeAndChassisAsync(string bookingCode, string chassisNumber)
    {
        var url =
            $"Reservation/GetBooking?bookingCode={Uri.EscapeDataString(bookingCode)}" +
            $"&chassisNumber={Uri.EscapeDataString(chassisNumber)}";

        var response = await httpClient.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new ResultDto<BookingDto[]>("", false)
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

        var result = await response.Content.ReadFromJsonAsync<ResultDto<BookingDto[]>>();
        return result ?? new ResultDto<BookingDto[]>("دریافت وضعیت خودرو با خطا مواجه شد.", false);
    }
}
