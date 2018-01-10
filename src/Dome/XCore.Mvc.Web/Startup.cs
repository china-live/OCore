using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using XCore.Logging;
using XCore.Modules.Extensions;
using XCore.Environment.Extensions.Manifests;
using XCore.Environment.Extensions;

namespace XCore.Mvc.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("logging.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"logging.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        private IServiceCollection _services;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(JavaScriptEncoder.Default);

            services.AddDistributedMemoryCache();
            services.AddSession();

            //services.AddThemingHost();
            services.AddManifestDefinition("Theme.txt", "theme");
            services.AddExtensionLocation("Themes");

            services.AddModules(configure =>
                {
                    configure.WithConfiguration(Configuration);
                    //configure.WithDefaultFeatures("XCore.Commons");
                }
            );

            _services = services;

            //IServiceProvider serviceProvider = _services.BuildServiceProvider();

            //Console.WriteLine(serviceProvider.GetType().ToString());
            //Console.WriteLine(serviceProvider.ToString());
            //foreach (var svc in _services)
            //{
            //    if (null != svc.ImplementationType)
            //    {
            //        Console.WriteLine("{0,-30}{1,-15}{2}", svc.ServiceType.Name, svc.Lifetime, svc.ImplementationType.Name);
            //        continue;
            //    }

            //    object instance = serviceProvider.GetService(svc.ServiceType);

            //    Console.WriteLine("{0,-30}{1,-15}{2}", svc.ServiceType.Name, svc.Lifetime, instance.GetType().Name);
            //}
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                PrintAllServices(app);
                //    loggerFactory.AddConsole(Configuration);
                //    loggerFactory.AddDebug();
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseNLogWeb(loggerFactory, env);

            app.UseModules();
        }

        private void PrintAllServices(IApplicationBuilder app)
        {
            app.Map("/allservices", builder => builder.Run(async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"<h1>所有服务{_services.Count}个</h1>" +
                    $"<table border=\"1\"><thead>" +
                    $"<tr><th>类型</th><th>生命周期</th><th>ImplementationType</th><th>ImplementationFactory</th><th>ImplementationInstance</th></tr>" +
                    $"</thead><tbody>");

                foreach (var svc in _services)
                {
                    await context.Response.WriteAsync("<tr>");
                    await context.Response.WriteAsync($"<td>{svc.ServiceType.FullName}</td>");
                    await context.Response.WriteAsync($"<td>{svc.Lifetime}</td>");
                    await context.Response.WriteAsync($"<td>{svc.ImplementationType?.FullName}</td>");
                    await context.Response.WriteAsync($"<td>{svc.ImplementationFactory?.ToString()}</td>");
                    await context.Response.WriteAsync($"<td>{svc.ImplementationInstance?.ToString()}</td>");
                    await context.Response.WriteAsync("</tr>");
                }
                await context.Response.WriteAsync("</tbody></table>");
            }));
        }

       
    }
}
