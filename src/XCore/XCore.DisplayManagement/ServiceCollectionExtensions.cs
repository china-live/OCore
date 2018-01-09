using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.DisplayManagement.Implementation;
using XCore.DisplayManagement.Theming;

namespace XCore.DisplayManagement
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTheming(this IServiceCollection services)
        {
 
            services.AddSingleton<IApplicationFeatureProvider<ViewsFeature>, ThemingViewsFeatureProvider>();
            services.AddScoped<IThemeManager, ThemeManager>();

            services.AddScoped<IDisplayHelperFactory, DisplayHelperFactory>();
            return services;
        }
    }
}
