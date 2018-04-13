using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using XCore.Modules;

namespace XCore.Mvc
{
    public class Startup : StartupBase
    {
        private readonly IServiceProvider _applicationServices;

        public Startup(IServiceProvider applicationServices)
        {
            _applicationServices = applicationServices;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcModules(_applicationServices).AddSessionStateTempDataProvider();
            services.AddResponseCompression();
            services.AddResponseCaching();
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.ConfigureModules(apb =>
            {
                apb.UseStaticFilesModules();
            });

            app.UseResponseCompression();
            app.UseResponseCaching();
        }
    }
}
