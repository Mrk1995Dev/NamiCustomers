using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Controllers
{
    public class BookingController(IBookingService bookingService) : MyBaseController
    {
        public async Task<IActionResult> Index(BookingTurnRequest? bookingTurnRequest)
        {
            if (bookingTurnRequest == null)
            {
                bookingTurnRequest = new BookingTurnRequest();
            }


            var result = await bookingService.GetBookingTurnAsync(bookingTurnRequest);

            if (!result.Succeeded)
            {
                SetError(result.Errors.FirstOrDefault());
                return RedirectToAction("Index", "Home");
            }

            return View(result.Data);
        }


    }
}
