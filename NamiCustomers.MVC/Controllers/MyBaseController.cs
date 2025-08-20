using Microsoft.AspNetCore.Mvc;

namespace NamiCustomers.MVC.Controllers
{
    public class MyBaseController : Controller
    {
        public IActionResult SetError(string errorResponse)
        {

            TempData["ErrorMessage"] = errorResponse;

            return RedirectToAction("Index", "Home");   
        }


    }
}
