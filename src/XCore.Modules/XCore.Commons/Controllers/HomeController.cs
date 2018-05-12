using Microsoft.AspNetCore.Mvc;

namespace XCore.Commons.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
