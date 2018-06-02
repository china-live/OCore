using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OCore.Environment.Shell;
using Microsoft.Extensions.Logging;

namespace OCore.DeferredTasks
{
    /// <summary>
    /// Executes any deferred tasks when the request is terminated.
    /// </summary>
    public class DeferredTaskMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IShellHost _OCoreHost;
        private readonly ILogger _logger;

        public DeferredTaskMiddleware(RequestDelegate next, IShellHost OCoreHost, ILogger<DeferredTaskMiddleware> logger)
        {
            _next = next;
            _OCoreHost = OCoreHost;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //_logger.LogInformation($"DeferredTaskMiddleware");

            await _next.Invoke(httpContext);

            //_logger.LogInformation($"httpContext.Features");
            // Register the shell settings as a custom feature.
            var shellSettings = httpContext.Features.Get<ShellSettings>();

            //_logger.LogInformation("eeewew2");

            // We only serve the next request if the tenant has been resolved.
            if (shellSettings != null)
            {
                var deferredTaskEngine = httpContext.RequestServices.GetService<IDeferredTaskEngine>();

                // Create a new scope only if there are pending tasks
                if (deferredTaskEngine != null && deferredTaskEngine.HasPendingTasks)
                {
                    // Dispose the scoped services for the current request, and create a new one
                    (httpContext.RequestServices as IDisposable).Dispose();

                    var shellContext = _OCoreHost.GetOrCreateShellContext(shellSettings);

                    if (!shellContext.Released)
                    {
                        //var scope = shellContext.CreateServiceScope();

                        //httpContext.RequestServices = scope.ServiceProvider;

                        //var context = new DeferredTaskContext(scope.ServiceProvider);
                        //await deferredTaskEngine.ExecuteTasksAsync(context);

                        //// We don't dispose the newly created request services scope as it will
                        //// be done by ModularTenantContainerMiddleware

                        using (var scope = shellContext.EnterServiceScope())
                        {
                            var context = new DeferredTaskContext(scope.ServiceProvider);
                            await deferredTaskEngine.ExecuteTasksAsync(context);
                        }
                    }
                }
            }
        }
    }
}
