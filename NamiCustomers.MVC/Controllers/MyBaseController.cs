using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NamiCustomers.MVC.Controllers
{
    public class MyBaseController: Controller
    {
        public IActionResult MyError(List<string>  errorResponse)
        {
             
            TempData["ErrorMessage"] = errorResponse;
            return RedirectToAction("Error", "Home");
        }
    }
}
