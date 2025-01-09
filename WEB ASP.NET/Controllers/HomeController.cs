using Microsoft.AspNetCore.Mvc;

namespace WEB_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

    }
}
