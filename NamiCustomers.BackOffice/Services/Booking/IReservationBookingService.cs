using NamiCustomers.Abstractions.Dtos;

namespace NamiCustomers.BackOffice.Services.Booking;

public interface IReservationBookingService
{
    Task<ResultDto<BookingResultDto[]>> GetBookingAsync(string bookingCode, string chassisNumber);
}
