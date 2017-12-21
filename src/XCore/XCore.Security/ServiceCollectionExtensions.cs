using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using XCore.Security.AuthorizationHandlers;

namespace XCore.Security
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds tenant level services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            //services.Configure<AuthorizationOptions>();
            services.AddAuthorization();
            services.AddScoped<IAuthorizationHandler, SuperUserHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            return services;
        }
    }
}