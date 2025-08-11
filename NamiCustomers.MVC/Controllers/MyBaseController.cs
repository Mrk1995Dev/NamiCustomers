using Microsoft.AspNetCore.Mvc;

namespace NamiCustomers.MVC.Controllers
{
    public class MyBaseController : Controller
    {
        public void SetError(string errorResponse)
        {

            TempData["ErrorMessage"] = errorResponse;
        }


    }
}
