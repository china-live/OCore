using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using XCore.Environment.Extensions.Features;
using XCore.Environment.Extensions.Loaders;
using XCore.Environment.Extensions.Manifests;

namespace XCore.Environment.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加扩展(模块)的注入服务，这些服务是Host级别的。
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddExtensionManagerHost(this IServiceCollection services)
        {
            services.AddSingleton<IManifestProvider, ManifestProvider>();
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<ManifestOptions>, ManifestOptionsSetup>());

            services.AddSingleton<IExtensionProvider, ExtensionProvider>();
            services.AddSingleton<IExtensionManager, ExtensionManager>();
            {
                services.AddSingleton<ITypeFeatureProvider, TypeFeatureProvider>();
                services.AddSingleton<IFeaturesProvider, FeaturesProvider>();

                services.TryAddEnumerable(
                    ServiceDescriptor.Transient<IConfigureOptions<ExtensionExpanderOptions>, ExtensionExpanderOptionsSetup>());


                services.AddSingleton<IExtensionLoader, AmbientExtensionLoader>();

                services.AddSingleton<IExtensionDependencyStrategy, ExtensionDependencyStrategy>();
                services.AddSingleton<IExtensionPriorityStrategy, ExtensionPriorityStrategy>();
            }

            return services;
        }

        //public static IServiceCollection AddExtensionManager(this IServiceCollection services)
        //{
        //    services.TryAddTransient<IFeatureHash, FeatureHash>();

        //    return services;
        //}

        /// <summary>
        /// 添加扩展（模块）所在的位置。
        /// 该配置是指定“应用程序主模块”需要在什么位置去加载“扩展（模块）”
        /// </summary>
        /// <param name="services"></param>
        /// <param name="subPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddExtensionLocation(this IServiceCollection services,string subPath)
        {
            return services.Configure<ExtensionExpanderOptions>(configureOptions: options =>
            {
                options.Options.Add(new ExtensionExpanderOption { SearchPath = subPath.Replace('\\', '/').Trim('/') });
            });
        }
    }
}
