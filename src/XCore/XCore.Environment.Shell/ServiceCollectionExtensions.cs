using XCore.Environment.Shell.Descriptor;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Environment.Shell.Descriptor.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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
    }
}
