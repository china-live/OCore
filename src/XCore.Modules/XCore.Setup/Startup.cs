using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using XCore.Modules;
using XCore.Setup.Services;

namespace XCore.Setup
{
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {

    //    }
    //}
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISetupService, SetupService>();
		}

		public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaRoute(
                name: "Setup",
                areaName: "XCore.Setup",
                template: "{controller=Setup}/{action=Index}/{id?}"
            );
        }
    }
}
