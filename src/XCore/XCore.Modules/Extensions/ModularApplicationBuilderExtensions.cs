using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XCore.Environment.Extensions;

namespace XCore.Modules.Extensions
{
    public static class ModularApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModules(this IApplicationBuilder app, Action<ModularApplicationBuilder> modules = null)
        {
            //app.UseMiddleware<ModularContainerMiddleware>();

            //app.ConfigureModules(modules);

            //app.UseMiddleware<ModularRouterMiddleware>();

            //return app;
            var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            env.ContentRootFileProvider = new CompositeFileProvider(
                new ModuleEmbeddedFileProvider(env),
                env.ContentRootFileProvider);

            // Ensure the shell tenants are loaded when a request comes in
            // and replaces the current service provider for the tenant's one.
            app.UseMiddleware<ModularContainerMiddleware>();

            app.ConfigureModules(modules);

            app.UseMiddleware<ModularRouterMiddleware>();

            return app;
        }

        //public static IApplicationBuilder UseModules(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes)

        /// <summary>
        /// 定义模块化开发时的扩展（模块）的配置项；
        /// 使用方式和标准asp.net core项目Startup中的Configure一致；
        /// 但也不是完全一样，因为在模块化开发时，由原来单一的配置变为多个配置。
        /// 加载顺序和模块Order值有关。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureModules(this IApplicationBuilder app, Action<ModularApplicationBuilder> modules)
        {
            var modularApplicationBuilder = new ModularApplicationBuilder(app);
            modules?.Invoke(modularApplicationBuilder);

            return app;
        }

        /// <summary>
        /// 定义模块化开发时扩展（模块）的静态文件读取；
        /// 模块化开发时，静态文件分布在各个模块中，需要自定义文件的读取方式。
        /// </summary>
        /// <param name="modularApp"></param>
        /// <returns></returns>
        public static ModularApplicationBuilder UseStaticFilesModules(this ModularApplicationBuilder modularApp)
        {
            modularApp.Configure(app =>
            {
                var extensionManager = app.ApplicationServices.GetRequiredService<IExtensionManager>();
                var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

                // TODO: configure the location and parameters (max-age) per module.配置每个模块的位置和参数（max-age）
                var availableExtensions = extensionManager.GetExtensions();
                foreach (var extension in availableExtensions)
                {
                    var contentPath = extension.ExtensionFileInfo.PhysicalPath != null
                        ? Path.Combine(extension.ExtensionFileInfo.PhysicalPath, "Content")
                        : null;

                    var contentSubPath = Path.Combine(extension.SubPath, "Content");

                    if (env.ContentRootFileProvider.GetDirectoryContents(contentSubPath).Exists)
                    {
                        IFileProvider fileProvider;
                        if (env.IsDevelopment())
                        {
                            var fileProviders = new List<IFileProvider>();
                            fileProviders.Add(new ModuleProjectContentFileProvider(env, contentSubPath));

                            if (contentPath != null)
                            {
                                fileProviders.Add(new PhysicalFileProvider(contentPath));
                            }
                            else
                            {
                                fileProviders.Add(new ModuleEmbeddedFileProvider(env, contentSubPath));
                            }

                            fileProvider = new CompositeFileProvider(fileProviders);
                        }
                        else
                        {
                            if (contentPath != null)
                            {
                                fileProvider = new PhysicalFileProvider(contentPath);
                            }
                            else
                            {
                                fileProvider = new ModuleEmbeddedFileProvider(env, contentSubPath);
                            }
                        }

                        app.UseStaticFiles(new StaticFileOptions
                        {
                            RequestPath = "/" + extension.Id,
                            FileProvider = fileProvider
                        });
                    }
                }
            });

            Console.Write("UseStaticFilesModules");

            return modularApp;
        }
    }
}
