using Microsoft.AspNetCore.Mvc;

namespace NbSites.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Index");
        }
    }
}
