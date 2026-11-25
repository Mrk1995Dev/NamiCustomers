using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.Application.Services.Book.Contract
{
    public interface IBookingService
    {
        Task<ResultDto<BookingResponse>> GetBookingAsync(string vinNumber);
        /// <summary>
        /// جستجوی وضعیت خودرو با شماره نوبت‌دهی و شماره شاسی (هر دو الزامی) از طریق Sevensoft GetBooking.
        /// </summary>
        Task<ResultDto<BookingResponse[]>> GetBookingByCodeAndChassisAsync(string bookingCode, string chassisNumber);
        Task<(bool, string)> CheckExistsReserveVinNumberAsync(string VinNumber);
        Task<(bool, string)> CheckExistsReservePhoneNumberAsync(string phoneNumber);
        Task<bool> CancelBookingAsync(Guid bookingId, string number);
        Task<ResultDto<InsertBookingResponse>> InsertBookingAsync(InsertBookingRequest request);
        Task<(bool, string)> CheckIsValidKilometerAsync(string VinNumber, int kilometer);
        Task<ResultDto<CityResponse[]>> GetAllCityAsync(Guid SubCountryId);
        Task<ResultDto<DealerResponse[]>> GetAllDealerAsync(Guid cityId);
    }
}