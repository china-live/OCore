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

            //modules(modularApplicationBuilder);
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

                // TODO: 配置每个模块的位置和参数（max-age）。
                var availableExtensions = extensionManager.GetExtensions();
                foreach (var extension in availableExtensions)
                {
                    //扩展（模块）的静态文件默认放在“Content”下
                    //先这样，以后可改为从配文件读取
                    var contentPath = Path.Combine(extension.ExtensionFileInfo.PhysicalPath, "Content");
                    var contentSubPath = Path.Combine(extension.SubPath, "Content");

                    if (Directory.Exists(contentPath))
                    {
                        IFileProvider fileProvider;
                        if (env.IsDevelopment())
                        {
                            fileProvider = new CompositeFileProvider(
                                new ModuleProjectContentFileProvider(env.ContentRootPath, contentSubPath),
                                new PhysicalFileProvider(contentPath));
                        }
                        else
                        {
                            fileProvider = new PhysicalFileProvider(contentPath);
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
