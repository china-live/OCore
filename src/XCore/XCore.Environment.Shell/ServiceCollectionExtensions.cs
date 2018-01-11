using XCore.Environment.Shell.Descriptor;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Environment.Shell.Descriptor.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using XCore.Environment.Shell.Data;

namespace XCore.Environment.Shell
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAllFeaturesDescriptor(this IServiceCollection services)
        {
            services.AddScoped<IShellDescriptorManager, AllFeaturesShellDescriptorManager>();

            return services;
        }

        public static IServiceCollection AddSetFeaturesDescriptor(this IServiceCollection services, IEnumerable<ShellFeature> shellFeatures)
        {
            services.AddSingleton<IShellDescriptorManager>(new SetFeaturesShellDescriptorManager(shellFeatures));

            return services;
        }

        /// <summary>
        ///  Host services to load site settings from the file system
        ///  从文件系统加载站点设置的主机服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sitesPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddSitesFolder(this IServiceCollection services)
        {
            services.AddSingleton<IShellSettingsConfigurationProvider, ShellSettingsConfigurationProvider>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();
            services.AddTransient<IConfigureOptions<ShellOptions>, ShellOptionsSetup>();

            return services;
        }

        /// <summary>
        /// Per-tenant services to store shell state and shell descriptors in the database.
        /// 在数据库中持久化存储每个租户的shell状态和配置信息。
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddShellDescriptorStorage(this IServiceCollection services)
        {
            services.AddScoped<IShellDescriptorManager, ShellDescriptorManager>();
            services.AddScoped<IShellStateManager, ShellStateManager>();
            services.AddScoped<IShellFeaturesManager, ShellFeaturesManager>();
            services.AddScoped<IShellDescriptorFeaturesManager, ShellDescriptorFeaturesManager>();

            return services;
        }
    }
}
