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
}
