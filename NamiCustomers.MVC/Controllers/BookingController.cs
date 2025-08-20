using Microsoft.AspNetCore.Mvc;

namespace NamiCustomers.MVC.Controllers
{
	public class BookingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
