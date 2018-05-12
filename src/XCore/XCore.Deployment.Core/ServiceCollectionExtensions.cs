using Microsoft.Extensions.DependencyInjection;
using XCore.Deployment.Core.Services;
using XCore.Deployment.Services;

namespace XCore.Deployment.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeploymentServices(this IServiceCollection services)
        {
            services.AddScoped<IDeploymentManager, DeploymentManager>();

            return services;
        }
    }
}
