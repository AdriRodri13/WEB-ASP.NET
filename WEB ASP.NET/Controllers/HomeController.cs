using Microsoft.AspNetCore.Mvc;

namespace WEB_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
