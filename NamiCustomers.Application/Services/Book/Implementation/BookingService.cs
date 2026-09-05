using NamiCustomers.Application.Services.Book.Contract;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.Application.Services.Book.Implementation
{
    public class BookingService(ISevenSoftService sevenSoftService) : IBookingService
    {
        public async Task<ResultDto<BookingResponse>> GetBookingAsync(string vinNumber)
        {
            return await sevenSoftService.GetBooking(vinNumber);
        }

        public async Task<(bool, string)> CheckExistsReserveVinNumberAsync(string VinNumber)
        {
            return await sevenSoftService.CheckExistsReserveVinNumber(VinNumber);
        }

        public async Task<(bool, string)> CheckExistsReservePhoneNumberAsync(string phoneNumber)
        {
            return await sevenSoftService.CheckExistsReservePhoneNumber(phoneNumber);
        }

        public async Task<bool> CancelBookingAsync(Guid bookingId, string number)
        {
            return await sevenSoftService.CancelBooking(bookingId, number);
        }

        public async Task<ResultDto<InsertBookingResponse>> InsertBookingAsync(InsertBookingRequest request)
        {
            return await sevenSoftService.InsertBooking(request);
        }

        public async Task<(bool, string)> CheckIsValidKilometerAsync(string VinNumber, int kilometer)
        {
            return await sevenSoftService.CheckIsValidKilometer(VinNumber, kilometer);
        }

        public async Task<ResultDto<CityResponse[]>> GetAllCityAsync(Guid SubCountryId)
        {
            SubCountryId = Guid.Parse("de5b7996-131d-44c5-88b9-fc3a511506a0");
            return await sevenSoftService.GetAllCity(SubCountryId);
        }

        public async Task<ResultDto<DealerResponse[]>> GetAllDealerAsync(Guid cityId)
        {
            return await sevenSoftService.GetAllDealer(cityId);
        }
    }
}
