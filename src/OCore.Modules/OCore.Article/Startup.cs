using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using OCore.Article.EntityFrameworkCore;
using OCore.EntityFrameworkCore;
using OCore.Modules;

namespace OCore.Article
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
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetService<AppDbContext>();

            services.AddActicle<Article>()
               .AddEntityFrameworkStores();

            services.AddTencentVod<TencentVod>()
               .AddEntityFrameworkStores();

        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            //app.UseAuthentication();
        }
    }
}
