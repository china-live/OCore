using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.Admin;

namespace XCore.Mvc.HelloWorld.Controllers
{
    [Admin]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
