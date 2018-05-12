using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using XCore.Environment.Shell.Descriptor;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Environment.Shell.Descriptor.Settings;

namespace XCore.Environment.Shell
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddHostingShellServices(this IServiceCollection services)
        //{
        //    services.AddSingleton<ShellHost>();
        //    services.AddSingleton<IShellHost>(sp => sp.GetRequiredService<ShellHost>());
        //    services.AddSingleton<IShellDescriptorManagerEventHandler>(sp => sp.GetRequiredService<ShellHost>());
        //    {
        //        // Use a single default site by default, i.e. if AddMultiTenancy hasn't been called before
        //        services.TryAddSingleton<IShellSettingsManager, SingleShellSettingsManager>();

        //        services.AddSingleton<IShellContextFactory, ShellContextFactory>();
        //        {
        //            services.AddSingleton<ICompositionStrategy, CompositionStrategy>();

        //            services.AddSingleton<IShellContainerFactory, ShellContainerFactory>();
        //        }
        //    }
        //    services.AddSingleton<IRunningShellTable, RunningShellTable>();

        //    return services;
        //}

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


    }
 
    
}
