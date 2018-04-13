using Microsoft.AspNetCore.Mvc;

namespace XCore.Mvc.HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
