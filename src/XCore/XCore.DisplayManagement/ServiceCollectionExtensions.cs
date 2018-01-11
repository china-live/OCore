using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.DisplayManagement.Descriptors;
using XCore.DisplayManagement.Events;
using XCore.DisplayManagement.Extensions;
using XCore.DisplayManagement.Implementation;
using XCore.DisplayManagement.Layout;
using XCore.DisplayManagement.LocationExpander;
using XCore.DisplayManagement.Theming;
using XCore.Environment.Extensions;
using XCore.Environment.Extensions.Features;
using XCore.Mvc.LocationExpander;
using static XCore.DisplayManagement.Descriptors.ShapeProviderExtensions;

namespace XCore.DisplayManagement
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds host level services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddThemingHost(this IServiceCollection services)
        {
            services.AddSingleton<IExtensionDependencyStrategy, ThemeExtensionDependencyStrategy>();
            services.AddSingleton<IFeatureBuilderEvents, ThemeFeatureBuilderEvents>();

            return services;
        }

        public static IServiceCollection AddTheming(this IServiceCollection services)
        {
 
            services.AddSingleton<IApplicationFeatureProvider<ViewsFeature>, ThemingViewsFeatureProvider>();
            services.AddScoped<IViewLocationExpanderProvider, ThemeAwareViewLocationExpanderProvider>();
            services.AddTransient<IShapeTableManager, DefaultShapeTableManager>();
            services.AddScoped<ILayoutAccessor, LayoutAccessor>();
            services.AddScoped<IThemeManager, ThemeManager>();

            services.AddScoped<IShapeFactory, DefaultShapeFactory>();
            services.AddScoped<IDisplayHelperFactory, DisplayHelperFactory>();
            return services;
        }
    }
}
