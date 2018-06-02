using Microsoft.Extensions.DependencyInjection;
using OCore.Deployment.Core.Services;
using OCore.Deployment.Services;

namespace OCore.Deployment.Core
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
