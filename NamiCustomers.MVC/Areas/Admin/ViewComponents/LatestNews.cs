using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.Nami;

namespace NamiCustomers.MVC.Areas.Admin.ViewComponents;

public class LatestNews(INamiKhodroService namiKhodroService) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var data = namiKhodroService.GetNamiNews().Result;
        return View(viewName: "LatestNews", data.OrderBy(c => c.date).Take(2).ToArray());
    }
}