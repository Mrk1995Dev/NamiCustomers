using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NamiCustomers.MVC.Services;
namespace NamiCustomers.MVC.Filters;

public class VinFilter(IHttpContextAccessor httpContextAccessor, ISubscriberService  subscriberService) : IActionFilter
{
    /*
     * Alternatively, you can apply it to a specific controller or action method by using the ServiceFilter attribute:
     [ServiceFilter(typeof(CheckActionFilter))]
    */

    public void OnActionExecuting(ActionExecutingContext context)
    {
        List<string> allewedActions = new() { "SignOut", "login", "Logout" };
        //List<string> allewedActions = new() { "SignOut", "login" };
       
		string actionName = context.RouteData.Values["action"].ToString();
        string controllerName =  context.RouteData.Values["controller"].ToString();
        // Check the action in the route request
        var destinationAction = $"/{controllerName}/{actionName}";
        if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var subscriber = subscriberService.CurrentSubscriber;

            if (!allewedActions.Contains(actionName))
            {
                if (controllerName!= "Vehicle" && (subscriber.VehicleModels==null || subscriber.VehicleModels.All(c=>c.IsDefault!=true)))
                {

                        context.Result = new RedirectToActionResult("Index","Vehicle" , null);
               
                }
            }
        }
       

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Code to run after the action executes
    }
}


