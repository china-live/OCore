using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using XCore.Environment.Shell;

namespace XCore.Modules
{
    public class ModularContainerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IShellHost _orchardHost;
        private readonly IRunningShellTable _runningShellTable;

        public ModularContainerMiddleware(
            RequestDelegate next,
            IShellHost orchardHost,
            IRunningShellTable runningShellTable,
            ILogger<ModularContainerMiddleware> logger)
        {
            _next = next;
            _orchardHost = orchardHost;
            _runningShellTable = runningShellTable;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("正在构建模块容器");
            }

            // Ensure all ShellContext are loaded and available.
            _orchardHost.Initialize();

            var shellSetting = _runningShellTable.Match(httpContext);

            // Register the shell settings as a custom feature.
            httpContext.Features.Set(shellSetting);

            //var se= JsonConvert.SerializeObject(shellSetting);
            //_logger.LogInformation(se);

            if (shellSetting != null)
            {
                var shellContext = _orchardHost.GetOrCreateShellContext(shellSetting);

                var existingRequestServices = httpContext.RequestServices;
                using (var scope = shellContext.EnterServiceScope())
                {
                    if (!shellContext.IsActivated)
                    {
                        lock (shellContext)
                        {
                            // The tenant gets activated here
                            if (!shellContext.IsActivated)
                            {
                                var tenantEvents = scope.ServiceProvider.GetServices<IModularTenantEvents>();

                                foreach (var tenantEvent in tenantEvents)
                                {
                                    tenantEvent.ActivatingAsync().Wait();
                                }

                                httpContext.Items["BuildPipeline"] = true;
                                shellContext.IsActivated = true;

                                foreach (var tenantEvent in tenantEvents.Reverse())
                                {
                                    tenantEvent.ActivatedAsync().Wait();
                                }
                            }
                        }
                    }

                    shellContext.RequestStarted();

                    try
                    {
                        await _next.Invoke(httpContext);
                    }
                    finally
                    {
                        shellContext.RequestEnded();

                        // Call all terminating events before releasing the shell context
                        if (shellContext.CanTerminate)
                        {
                            var tenantEvents = scope.ServiceProvider.GetServices<IModularTenantEvents>();

                            foreach (var tenantEvent in tenantEvents)
                            {
                                await tenantEvent.TerminatingAsync();
                            }

                            foreach (var tenantEvent in tenantEvents.Reverse())
                            {
                                await tenantEvent.TerminatedAsync();
                            }

                            shellContext.Dispose();
                        }
                    }
                }
            }
        }
    }
}
