using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace NamiCustomers.MVC.Controllers
{
    public class MyBaseController: Controller
    {
        public void SetError(List<string>  errorResponse)
        {
             
            TempData["ErrorMessage"] = errorResponse;
        }

        
    }
}
