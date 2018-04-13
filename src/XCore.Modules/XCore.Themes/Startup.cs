using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using XCore.Themes.Services;
using System;
using XCore.DisplayManagement.Theming;
using XCore.Modules;

namespace XCore.Themes
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IThemeSelector, SafeModeThemeSelector>();
            services.AddScoped<IThemeSelector, SiteThemeSelector>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }
    }
}
