using Microsoft.AspNetCore.Mvc;

namespace OCore.Commons.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
