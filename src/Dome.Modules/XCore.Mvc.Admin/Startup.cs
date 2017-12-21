using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using XCore.Modules;
using XCore.Security.Permissions;
using XCore.UEditor;

namespace XCore.Mvc.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }

    public class Startup : StartupBase
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<IPermissionProvider, Permissions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(_configuration["Sample"]))
            {
                throw new Exception(":(");
            }
 
            routes.MapAreaRoute
            (
                name: "Admin",
                areaName: "XCore.Mvc.Admin",
                //template: "Admin/{controller}/{action}",
                template: "Admin/{controller=Home}/{action=Index}/{id?}"
                //defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
