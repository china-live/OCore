using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.DisplayManagement.Theming;

namespace XCore.DisplayManagement
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTheming(this IServiceCollection services)
        {
            services.AddScoped<IThemeManager, ThemeManager>();
            return services;
        }
    }
}
