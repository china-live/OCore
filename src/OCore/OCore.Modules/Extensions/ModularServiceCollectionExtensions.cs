using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OCore.Environment.Extensions;
using OCore.Environment.Extensions.Manifests;
using OCore.Environment.Shell;
using OCore.Environment.Shell.Descriptor;
using OCore.Environment.Shell.Descriptor.Models;
using OCore.Environment.Shell.Descriptor.Settings;
using OCore.Modules;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ModularServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块服务到<see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
        /// </summary>
        public static IServiceCollection AddModules(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            services.AddWebHost();
            services.AddManifestDefinition("module");

            // ModularTenantRouterMiddleware which is configured with UseModules() calls UserRouter() which requires the routing services to be
            // registered. This is also called by AddMvcCore() but some applications that do not enlist into MVC will need it too.
            services.AddRouting();
 
            // Use a single tenant and all features by default默认情况下使用单个租户，并且拥有全部功能
            services.AddAllFeaturesDescriptor();

            // Let the app change the default tenant behavior and set of features 在应用中配置覆盖默认租户和功能
            configure?.Invoke(services);

            // Registers the application feature
            services.AddTransient(sp =>
            {
                return new ShellFeature(sp.GetRequiredService<IHostingEnvironment>().ApplicationName);
            });

            // Register the list of services to be resolved later on 注册稍后要解决的服务列表
            services.AddSingleton(_ => services);

            return services;
        }

        ///// <summary>
        ///// 注册模块中要使用的配置文件选项
        ///// </summary>
        //public static ModularServiceCollection WithConfiguration(this ModularServiceCollection modules, IConfiguration configuration)
        //{
        //    // Register the configuration object for modules to register options with it
        //    if (configuration != null)
        //    {
        //        modules.Configure(services => services.AddSingleton<IConfiguration>(configuration));
        //    }

        //    return modules;
        //}

        /// <summary>
        /// Registers a default tenant with a set of features that are used to setup and configure the actual tenants.
        /// For instance you can use this to add a custom Setup module.
        /// 向默认租户注册一组用于设置和配置实际租户的功能。
        /// 例如，您可以使用它来添加自定义安装模块。
        /// </summary>
        public static IServiceCollection WithDefaultFeatures(this IServiceCollection services, params string[] featureIds)
        {
            foreach (var featureId in featureIds)
            {
                services.AddTransient(sp => new ShellFeature(featureId));
            }

            return services;
        }

        /// <summary>
        /// Registers tenants defined in configuration.
        /// 注册配置中定义的租户。在tenants.json中配置并定义租户及所拥有的功能。 
        /// </summary>
        public static IServiceCollection WithTenants(this IServiceCollection services)
        {
            services.AddSingleton<IShellSettingsConfigurationProvider, FileShellSettingsConfigurationProvider>();
            services.AddScoped<IShellDescriptorManager, FileShellDescriptorManager>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();
            services.AddScoped<ShellSettingsWithTenants>();

            return services;
        }

        /// <summary>
        /// Registers a single tenant with the specified set of features.
        /// 注册具有指定功能的单个租户。
        /// </summary>
        public static IServiceCollection WithFeatures(this IServiceCollection services,params string[] featureIds)
        {
            services.WithDefaultFeatures(featureIds);
            services.AddSetFeaturesDescriptor();

            return services;
        }

        public static IServiceCollection AddWebHost(
            this IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();
            services.AddLocalization();
            services.AddHostingShellServices();
            services.AddExtensionManagerHost();
            services.AddWebEncoders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClock, Clock>();

            services.AddSingleton<IPoweredByMiddlewareOptions, PoweredByMiddlewareOptions>();
            services.AddScoped<IModularTenantRouteBuilder, ModularTenantRouteBuilder>();

            return services;
        }
    }
}
