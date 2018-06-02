using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using OCore.Features.Recipes.Executors;
using OCore.Modules;
using OCore.Recipes;

namespace OCore.Features
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddRecipeExecutionStep<FeatureStep>();
            //services.AddScoped<IPermissionProvider, Permissions>();
            //services.AddScoped<IModuleService, ModuleService>();
            //services.AddScoped<INavigationProvider, AdminMenu>();


            //services.AddTransient<IDeploymentSource, AllFeaturesDeploymentSource>();
            //services.AddSingleton<IDeploymentStepFactory>(new DeploymentStepFactory<AllFeaturesDeploymentStep>());
            //services.AddScoped<IDisplayDriver<DeploymentStep>, AllFeaturesDeploymentStepDriver>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }
    }
}
