using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using OCore.Environment.Extensions;
using OCore.Modules;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Enables multi-tenant requests support for the current path.
        /// </summary>
        public static IApplicationBuilder UseOrchardCore(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null) {
            var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            env.ContentRootFileProvider = new CompositeFileProvider(
                new ModuleEmbeddedFileProvider(env),
                env.ContentRootFileProvider);

            app.UseMiddleware<PoweredByMiddleware>();

            // Ensure the shell tenants are loaded when a request comes in
            // and replaces the current service provider for the tenant's one.
            app.UseMiddleware<ModularTenantContainerMiddleware>();

            configure?.Invoke(app);

            app.UseMiddleware<ModularTenantRouterMiddleware>();

            return app;
        }

        //public static IApplicationBuilder UseModules(this IApplicationBuilder app, Action<IApplicationBuilder> modules = null)
        //{
        //    var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

        //    env.ContentRootFileProvider = new CompositeFileProvider(
        //        new ModuleEmbeddedFileProvider(env),
        //        env.ContentRootFileProvider);

        //    // Ensure the shell tenants are loaded when a request comes in
        //    // and replaces the current service provider for the tenant's one.
        //    app.UseMiddleware<PoweredByMiddleware>();
        //    app.UseMiddleware<ModularTenantContainerMiddleware>();

        //    app.ConfigureModules(modules);

        //    app.UseMiddleware<ModularTenantRouterMiddleware>();

        //    return app;
        //}

        //public static IApplicationBuilder ConfigureModules(this IApplicationBuilder app, Action<IApplicationBuilder> modules)
        //{
        //    modules?.Invoke(app);
        //    return app;
        //}

        //public static IApplicationBuilder UseStaticFilesModules(this IApplicationBuilder app)
        //{
        //    var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

        //    IFileProvider fileProvider;
        //    if (env.IsDevelopment())
        //    {
        //        var fileProviders = new List<IFileProvider>();
        //        fileProviders.Add(new ModuleProjectStaticFileProvider(env));
        //        fileProviders.Add(new ModuleEmbeddedStaticFileProvider(env));
        //        fileProvider = new CompositeFileProvider(fileProviders);
        //    }
        //    else
        //    {
        //        fileProvider = new ModuleEmbeddedStaticFileProvider(env);
        //    }

        //    app.UseStaticFiles(new StaticFileOptions
        //    {
        //        RequestPath = "",
        //        FileProvider = fileProvider
        //    });

        //    return app;
        //}
    }
}