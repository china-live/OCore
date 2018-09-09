using Microsoft.Extensions.Options;
using OCore.Environment.Shell;
using OCore.Environment.Shell.Descriptor;
using OCore.Environment.Shell.Descriptor.Models;
using OCore.Environment.Shell.Descriptor.Settings;

namespace Microsoft.Extensions.DependencyInjection {
    public static class OrchardCoreBuilderExtensions {
        /// <summary>
        /// Registers at the host level a set of features which are always enabled for any tenant.
        /// 在主机级别注册一组始终为任何租户启用的功能。
        /// </summary>
        public static OCoreBuilder AddGlobalFeatures(this OCoreBuilder builder, params string[] featureIds) {
            foreach (var featureId in featureIds) {
                builder.ApplicationServices.AddTransient(sp => new ShellFeature(featureId, alwaysEnabled: true));
            }

            return builder;
        }

        /// <summary>
        /// Registers at the tenant level a set of features which are always enabled.
        /// 在租户级别注册一组始终启用的功能。
        /// </summary>
        public static OCoreBuilder AddTenantFeatures(this OCoreBuilder builder, params string[] featureIds) {
            builder.ConfigureServices(services => {
                foreach (var featureId in featureIds) {
                    services.AddTransient(sp => new ShellFeature(featureId, alwaysEnabled: true));
                }
            });

            return builder;
        }

        /// <summary>
        /// Registers a default tenant with a set of features that are used to setup and configure the actual tenants.
        /// For instance you can use this to add a custom Setup module.
        /// 使用一组用于设置和配置实际租户的功能注册默认租户。例如，您可以使用它来添加自定义安装模块。
        /// </summary>
        public static OCoreBuilder AddSetupFeatures(this OCoreBuilder builder, params string[] featureIds) {
            foreach (var featureId in featureIds) {
                builder.ApplicationServices.AddTransient(sp => new ShellFeature(featureId));
            }

            return builder;
        }

        /// <summary>
        /// Registers tenants defined in configuration.
        /// 注册配置中定义的租户。
        /// </summary>
        public static OCoreBuilder WithTenants(this OCoreBuilder builder) {
            var services = builder.ApplicationServices;

            services.AddSingleton<IShellSettingsConfigurationProvider, FileShellSettingsConfigurationProvider>();
            services.AddScoped<IShellDescriptorManager, FileShellDescriptorManager>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();
            services.AddTransient<IConfigureOptions<ShellOptions>, ShellOptionsSetup>();
            services.AddScoped<ShellSettingsWithTenants>();

            return builder;
        }

        /// <summary>
        /// Registers a single tenant with the specified set of features.
        /// 使用指定的功能集注册单个租户。
        /// </summary>
        public static OCoreBuilder WithFeatures(this OCoreBuilder builder, params string[] featureIds) {
            foreach (var featureId in featureIds) {
                builder.ApplicationServices.AddTransient(sp => new ShellFeature(featureId));
            }

            builder.ApplicationServices.AddSetFeaturesDescriptor();

            return builder;
        }
    }
}
