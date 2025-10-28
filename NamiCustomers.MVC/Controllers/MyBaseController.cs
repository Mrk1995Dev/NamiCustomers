using Microsoft.AspNetCore.Mvc;

namespace NamiCustomers.MVC.Controllers
{
    public class MyBaseController : Controller
    {
        public IActionResult SetError(string errorResponse)
        {
            TempData["ErrorMessage"] = errorResponse;
            HttpContext.Session.SetString("ErrorMessage", errorResponse);
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LoginByMobile", "Account");
        }
        public void SetModelStateError()
        {
            string errorResponse = string.Join(",", ModelState.Values.Select(c => c.Errors.Select(r => r.ErrorMessage).FirstOrDefault()).ToList());
            TempData["ErrorMessage"] = errorResponse;
            HttpContext.Session.SetString("ErrorMessage", errorResponse);
        }
    }
}
