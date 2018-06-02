using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OCore.Mvc.Test.Controllers
{
    public class TestController : Controller {
        private readonly ILogger _logger;

        public TestController(
            ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
