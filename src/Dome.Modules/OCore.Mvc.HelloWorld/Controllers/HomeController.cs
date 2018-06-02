using Microsoft.AspNetCore.Mvc;

namespace OCore.Mvc.HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
