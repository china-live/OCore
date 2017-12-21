using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.Environment.Extensions;
using XCore.Environment.Extensions.Manifests;
using XCore.Environment.Shell;
using XCore.Environment.Shell.Descriptor.Models;

namespace XCore.Modules.Extensions
{
    public static class ModularServiceCollectionExtensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services, Action<ModularServiceCollection> configure = null)
        {
            services.AddWebHost();
            services.AddManifestDefinition("Module.txt", "module");
            services.AddExtensionLocation("Packages");

            // ModularRouterMiddleware which is configured with UseModules() calls UserRouter() which requires the routing services to be
            // registered. This is also called by AddMvcCore() but some applications that do not enlist into MVC will need it too.
            // 上面的英文大意是：ModularRouterMiddleware 是用在使用MVC框架时的路由处理，但有的应用可能不需要引入MVC框架。
            services.AddRouting();

            //services.AddSingleton<IEntityManager>(new EfInit().GetDefaultEntityManager());




            var modularServiceCollection = new ModularServiceCollection(services);

            // Use a single tenant and all features by default
            modularServiceCollection.Configure(internalServices =>
                internalServices.AddAllFeaturesDescriptor()
            );

            // Let the app change the default tenant behavior and set of features
            configure?.Invoke(modularServiceCollection);

            // Register the list of services to be resolved later on
            services.AddSingleton(_ => services);

            return services;
        }

        public static ModularServiceCollection WithConfiguration(this ModularServiceCollection modules, IConfiguration configuration)
        {
            // Register the configuration object for modules to register options with it
            if (configuration != null)
            {
                modules.Configure(services => services.AddSingleton<IConfiguration>(configuration));
            }

            return modules;
        }

        /// <summary>
        /// Registers a default tenant with a set of features that are used to setup and configure the actual tenants.
        /// For instance you can use this to add a custom Setup module.
        /// </summary>
        public static ModularServiceCollection WithDefaultFeatures(this ModularServiceCollection modules, params string[] featureIds)
        {
            modules.Configure(services =>
            {
                foreach (var featureId in featureIds)
                {
                    services.AddTransient(sp => new ShellFeature(featureId));
                };
            });

            return modules;
        }

        public static IServiceCollection AddWebHost(this IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();
            services.AddLocalization();
            services.AddHostingShellServices();
            services.AddExtensionManagerHost();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IClock, Clock>();

            services.AddScoped<IModularRouteBuilder, ModularRouteBuilder>();

            return services;
        }
    }
}
