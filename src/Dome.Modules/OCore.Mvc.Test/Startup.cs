using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using OCore.Modules;

namespace OCore.Mvc.Test
{
    public class Startup : StartupBase
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(_configuration["Sample"]))
            {
                throw new Exception(":(");
            }

            //routes.MapAreaRoute
            //(
            //    name: "Test",
            //    areaName: "OCore.Mvc.Test",
            //    template: "Test/{controller=Home}/{action=Index}/{id?}"
            //);
            routes.MapAreaRoute(
                name: "Home",
                areaName: "OCore.Demo",
                template: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
