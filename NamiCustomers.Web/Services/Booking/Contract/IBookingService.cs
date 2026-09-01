using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Web.Services.Booking.Dto;

namespace NamiCustomers.Web.Services.Booking.Contract;

public interface IBookingService
{
    Task<ResultDto<BookingDto>> GetBookByVinNumberAsync(string vinNumber);
}
