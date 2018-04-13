using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using XCore.Modules;

namespace XCore.Admin
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            //services.AddNavigation();

            //services.Configure<MvcOptions>((options) =>
            //{
            //    //options.Filters.Add(typeof(AdminZoneFilter));
            //    //options.Filters.Add(typeof(LayerFilter));
            //    //options.Filters.Add(typeof(AdminMenuFilter));
            //});

            //services.AddScoped<IPermissionProvider, Permissions>();
            //services.AddScoped<IThemeSelector, AdminThemeSelector>();
            //services.AddScoped<IAdminThemeService, AdminThemeService>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaRoute(
                name: "Adming",
                areaName: "XCore.Admin",
                template: "adming",
                defaults: new { controller = "Admin", action = "Index" }
            );
        }
    }
}
