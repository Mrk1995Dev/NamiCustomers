using Microsoft.Extensions.DependencyInjection;
using NamiCustomers.BackOffice.Services.Booking;

namespace NamiCustomers.BackOffice;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers BackOffice reservation/vehicle-status HTTP client (Reservation/GetBooking).
    /// <paramref name="apiBaseUrl"/> should be the BackOfficeAPI base including api version, e.g. https://host/api/v1/
    /// </summary>
    public static IServiceCollection AddBackOfficeReservationBooking(
        this IServiceCollection services,
        string apiBaseUrl)
    {
        services.AddHttpClient<IReservationBookingService, ReservationBookingService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl.TrimEnd('/') + "/");
        });
        return services;
    }
}
