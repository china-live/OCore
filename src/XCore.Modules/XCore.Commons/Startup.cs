using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using XCore.BackgroundTasks;
using XCore.DeferredTasks;
using XCore.EntityFrameworkCore;
using XCore.Environment.Cache;
using XCore.Environment.Shell;
using XCore.Environment.Shell.EntityFrameworkCore;
using XCore.Modules;

namespace XCore.Commons
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        //public override int Order => -1;

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDeferredTasks();

            //var serviceProvider = services.BuildServiceProvider();
            //var defaultConnection = serviceProvider.GetService<IConfiguration>().GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkCore(/*defaultConnection*/);

            services.AddBackgroundTasks();
            //services.AddResourceManagement();
            //services.AddGeneratorTagFilter();
            services.AddCaching();
            services.AddShellDescriptorStorage().AddEntityFrameworkStores();
            //services.AddExtensionManager();
            //services.AddTheming();
            //services.AddLiquidViews();
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            //serviceProvider.AddTagHelpers(typeof(ResourcesTagHelper).GetTypeInfo().Assembly);
            //serviceProvider.AddTagHelpers(typeof(ShapeTagHelper).GetTypeInfo().Assembly);
        }
    }

    ///// <summary>
    ///// Deferred tasks middleware is registered early as it has to run very late.
    ///// </summary>
    //public class DeferredTasksStartup : StartupBase
    //{
    //    public override int Order => -50;

    //    public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
    //    {
    //        app.AddDeferredTasks();
    //    }
    //}
}
