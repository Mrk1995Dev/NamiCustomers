using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.MVC.Filters;
using NamiCustomers.MVC.Services;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Controllers
{
    [ServiceFilter(typeof(VinFilter))]
    public class BookingController(IBookingService bookingService, ISevenSoftService sevenSoftService, ISubscriberService subscriberService, IVehicleService vehicleService) : MyBaseController
    {
        [ServiceFilter(typeof(VinFilter))]
        public async Task<IActionResult> BookingIndex(BookingTurnRequest? bookingTurnRequest)
        {
            if (bookingTurnRequest == null || bookingTurnRequest.CountryId==Guid.Empty)
            {
                bookingTurnRequest = new BookingTurnRequest();
            }


            var result = await bookingService.GetBookingTurnAsync(bookingTurnRequest);

            if (!result.Succeeded)
            {
                SetError(result.Errors.FirstOrDefault());
                return RedirectToAction("Index","Home");
            }

            return View("Index",result.Data);
        }
        public async Task<IActionResult> Register(BookingTurnRequest request)
        {
            var result = (await bookingService.GetBookingTurnAsync(request)).Data;
            var subscriber = subscriberService.CurrentSubscriber;
            var vehicle = subscriber.VehicleModels.FirstOrDefault(c => c.IsDefault);
            var advisors = await sevenSoftService.GetAllServerGroupRepairAdvisers(request.ServerGroupDateId, request.ServerGroupId);
            var subscriberChassisAllocationId = (await sevenSoftService.GetSubscriberChassisAllocation(vehicle.VinNumber)).Data.UniqueId;
            //var date = result.ServerGroupDates.FirstOrDefault(c => c.Value == request.ServerGroupDateId.ToString()).Value;
            //var hour = result.ServerGroupTimes.FirstOrDefault(c => c.Value == request.ServerGroupTimeId.ToString()).Value.Split(":")[0];
            //var min = result.ServerGroupTimes.FirstOrDefault(c => c.Value == request.ServerGroupTimeId.ToString()).Value.Split(":")[1];
            InsertBookingRequest bookingRequest = new InsertBookingRequest
            {
                BookingIsCompanyCustomer = true,
                BookingSubscriberChassisAllocationId = subscriberChassisAllocationId, //new Guid("4211E2E3-C1AE-4989-9C50-4EEC82DEE31F"),//todo hamed
                BookingFirstName = subscriber.Name,
                BookingLastName = subscriber.Family,
                BookingVinNumber = vehicle.VinNumber,
                BookingVehicleModelId = vehicle.VehicleModelIdSevenSoft.Value,
                BookingKilometer = request.Kilometer,
                CheckBookingKilometer = true,
                BookingTime = request.ServerGroupTimeId,
                BookingServerGroupId = request.ServerGroupId,
                BookingCustomerStatementList = new[]
                {
                    new Bookingcustomerstatementlist
                    {
                        BookingCustomerStatementDescription=request.Description,
                        DefaultCustomerDescription=request.Description,
                        DefaultCustomerDescriptionName=request.Description,
                        Approved=true
                    }
                },
                BookingRepairAdviserId = advisors.FirstOrDefault().Value,
                WorkShopTimeTableId = request.ServerGroupDateId,
                AddBookinglock = true,
                AddBookingRepairPlaceEnum = 1,
                BranchId = request.BranchId,
                Latitude = 0,
                Longitude = 0,
                BookingDate = DateTime.Now,
                IsBookingFinal = true
            };
            var insertResult = await sevenSoftService.InsertBooking(bookingRequest);

            if (!insertResult.Succeeded)
            {
                SetError(insertResult.Message);
                return RedirectToAction("BookingIndex");
            }
            var insertBookingResponse = insertResult.Data;

            return Json(insertBookingResponse);
        }

        public async Task<IActionResult> Index2()
        {
            return View();
        }


        public async Task<IActionResult> CancelBooking(Guid bookingId  , string vinNumber )
        {
            var result = await sevenSoftService.CancelBooking(bookingId, vinNumber);
            if (!result)
            {
                SetError(Infrastucture.Properties.Resources.errFailed);
            }
            return RedirectToAction("BookingIndex");
        }

        public async Task<IActionResult> CancelBooking(string vinNumber)
        {
            var result = await sevenSoftService.GetBooking(vinNumber);
            if (!result.Succeeded)
            {
                SetError(result.Message);
            }
            return RedirectToAction("BookingIndex");
        }


        public async Task<IActionResult> Refresh(BookingTurnRequest? bookingTurnRequest)
        {
            if (bookingTurnRequest == null)
            {
                bookingTurnRequest = new BookingTurnRequest();
            }


            var result = await bookingService.GetBookingTurnAsync(bookingTurnRequest);

            if (!result.Succeeded)
            {
                SetError(result.Errors.FirstOrDefault());
                // Return a JSON result to the client
                return Json(new
                {
                    success = false,
                    Data = new BookingTurnResponse { SubCountryId = bookingTurnRequest.SubCountryId }
                });
            }
            // Return a JSON result to the client
            return Json(new
            {
                success = true,
                Data = result.Data
            });
        }


    }
}
