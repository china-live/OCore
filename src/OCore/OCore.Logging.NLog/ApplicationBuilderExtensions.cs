using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.LayoutRenderers;
using NLog.Web;
using System.IO;

namespace OCore.Logging
{
    public static class ApplicationBuilderExtensions
    {
        //public static IApplicationBuilder UseNLogWeb(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        //{
        //    LayoutRenderer.Register<TenantLayoutRenderer>(TenantLayoutRenderer.LayoutRendererName);
        //    loggerFactory.AddNLog();
        //    app.AddNLogWeb();

        //    return app;
        //}

        //public static IApplicationBuilder UseNLogWeb(this IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        //{
        //    env.ConfigureNLog($"{env.ContentRootPath}{Path.DirectorySeparatorChar}NLog.config");
        //    LayoutRenderer.Register<TenantLayoutRenderer>(TenantLayoutRenderer.LayoutRendererName);
        //    loggerFactory.AddNLog();
        //    app.AddNLogWeb();
        //    LogManager.Configuration.Variables["configDir"] = env.ContentRootPath;

           
        //    return app;
        //}

        public static IWebHostBuilder UseNLogWeb(this IWebHostBuilder builder)
        {
            LayoutRenderer.Register<TenantLayoutRenderer>(TenantLayoutRenderer.LayoutRendererName);
            builder.UseNLog();
            builder.ConfigureAppConfiguration((context, configuration) =>
            {
                var environment = context.HostingEnvironment;
                environment.ConfigureNLog($"{environment.ContentRootPath}{Path.DirectorySeparatorChar}NLog.config");
                LogManager.Configuration.Variables["configDir"] = environment.ContentRootPath;
            });

            return builder;
        }
    }
}
