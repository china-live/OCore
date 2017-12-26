using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XCore.Modules
{
    /// <summary>
    /// 定制 <see cref="IFileProvider"/> 实现读取“模块项目”中的静态文件，该项只会在开发环境中使用 
    /// </summary>
    public class ModuleProjectContentFileProvider : IFileProvider
    {
        private static Dictionary<string, string> _paths;
        private static object _synLock = new object();

        private string _contentRoot;

        public ModuleProjectContentFileProvider(IHostingEnvironment environment, string contentPath)
        {
            _contentRoot = NormalizePath(contentPath) + "/";

            if (_paths != null)
            {
                return;
            }

            lock (_synLock)
            {
                if (_paths == null)
                {
                    var assets = new List<Asset>();
                    var application = environment.GetApplication();

                    foreach (var name in application.ModuleNames)
                    {
                        var module = environment.GetModule(name);

                        if (module.Assembly == null || Path.GetDirectoryName(module.Assembly.Location)
                            != Path.GetDirectoryName(application.Assembly.Location))
                        {
                            continue;
                        }

                        var contentRoot = Application.ModulesRoot + name + '/' + Module.ContentRoot;

                        assets.AddRange(module.Assets.Where(a => a.ModuleAssetPath
                            .StartsWith(contentRoot, StringComparison.Ordinal)));
                    }

                    _paths = assets.ToDictionary(a => a.ModuleAssetPath, a => a.ProjectAssetPath);
                }
            }
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return NotFoundDirectoryContents.Singleton;
        }
 
        public IFileInfo GetFileInfo(string subpath)
        {
            if (subpath == null)
            {
                return new NotFoundFileInfo(subpath);
            }

            var path = _contentRoot + NormalizePath(subpath);

            if (_paths.ContainsKey(path))
            {
                return new PhysicalFileInfo(new FileInfo(_paths[path]));
            }

            return new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }

        private string NormalizePath(string path)
        {
            return path.Replace('\\', '/').Trim('/');
        }
    }
}
