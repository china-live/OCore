using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using XCore.Modules;
using XCore.Recipes;
using XCore.Settings.Recipes;
using XCore.Settings.Services;
using XCore.Setup.Events;

namespace XCore.Settings
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISetupEventHandler, SetupEventHandler>();
            //services.AddScoped<IPermissionProvider, Permissions>();

            services.AddRecipeExecutionStep<SettingsStep>();
            services.AddScoped<ISiteSettingStore, SiteSettingStore>().AddEntityFrameworkSqlServer();
            services.AddSingleton<ISiteService, SiteService>();

            //// Site Settings editor
            //services.AddScoped<IDisplayManager<ISite>, DisplayManager<ISite>>();
            //services.AddScoped<IDisplayDriver<ISite>, DefaultSiteSettingsDisplayDriver>();
            //services.AddScoped<IDisplayDriver<ISite>, ButtonsSettingsDisplayDriver>();
            //services.AddScoped<INavigationProvider, AdminMenu>();

            //services.AddScoped<ILiquidTemplateEventHandler, SiteLiquidTemplateEventHandler>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // Admin
            routes.MapAreaRoute(
                name: "AdminSettings",
                areaName: "XCore.Settings",
                template: "Admin/Settings/{groupId}",
                defaults: new { controller = "Admin", action = "Index" }
            );
        }
    }
}
