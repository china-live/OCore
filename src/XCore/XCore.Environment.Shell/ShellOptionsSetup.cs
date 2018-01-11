using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace XCore.Environment.Shell
{
    /// <summary>
    /// Sets up default options for <see cref="ShellOptions"/>.
    /// </summary>
    public class ShellOptionsSetup : IConfigureOptions<ShellOptions>
    {
        private const string AppData = "XCore_APP_DATA";
        private const string DefaultAppDataPath = "App_Data";
        private const string DefaultSitesPath = "Sites";

        private readonly IHostingEnvironment _hostingEnvironment;

        public ShellOptionsSetup(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void Configure(ShellOptions options)
        {
            var appData = System.Environment.GetEnvironmentVariable(AppData);

            if (!String.IsNullOrEmpty(appData))
            {
                options.ShellsApplicationDataPath = Path.Combine(_hostingEnvironment.ContentRootPath, appData);
            }
            else
            {
                options.ShellsApplicationDataPath = Path.Combine(_hostingEnvironment.ContentRootPath, DefaultAppDataPath);
            }

            options.ShellsContainerName = DefaultSitesPath;
        }
    }
}
