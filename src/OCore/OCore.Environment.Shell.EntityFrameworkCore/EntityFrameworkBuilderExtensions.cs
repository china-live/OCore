using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using OCore.Environment.Shell.Data;
using OCore.Environment.Shell.Descriptor;
using OCore.Environment.Shell.Descriptor.Models;
using OCore.Environment.Shell.State;

namespace OCore.Environment.Shell.EntityFrameworkCore
{
    public static class EntityFrameworkBuilderExtensions
    {
        public static ShellBuilder AddEntityFrameworkStores(this ShellBuilder builder)
        {
            builder.AddShellDescriptorStore<ShellDescriptorStore>();
            builder.AddShellStateStore<ShellStateStore>();
            return builder;
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
        public static ShellBuilder AddShellDescriptorStorage(this IServiceCollection services)
        {
            services.AddScoped<IShellDescriptorManager, ShellDescriptorManager>();
            services.AddScoped<IShellStateManager, ShellStateManager>();
            services.AddScoped<IShellFeaturesManager, ShellFeaturesManager>();
            services.AddScoped<IShellDescriptorFeaturesManager, ShellDescriptorFeaturesManager>();

            return new ShellBuilder(typeof(ShellDescriptor), typeof(ShellState), services);
        }
    }

    public class ShellBuilder
    {
        public ShellBuilder(Type shellDescriptor, Type shellState, IServiceCollection services)
        {
            Services = services;
            ShellDescriptor = shellDescriptor;
            ShellState = shellState;
        }

        public IServiceCollection Services { get; private set; }

        public Type ShellDescriptor { get; private set; }

        public Type ShellState { get; private set; }

        private ShellBuilder AddScoped(Type serviceType, Type concreteType)
        {
            Services.AddScoped(serviceType, concreteType);
            return this;
        }

        public ShellBuilder AddShellDescriptorStore<T>()
        {
            return AddScoped(typeof(IShellDescriptorStore<>).MakeGenericType(ShellDescriptor), typeof(T));
        }

        public ShellBuilder AddShellStateStore<T>()
        {
            return AddScoped(typeof(IShellStateStore<>).MakeGenericType(ShellState), typeof(T));
        }
    }
}
