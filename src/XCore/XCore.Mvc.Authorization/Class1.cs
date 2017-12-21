using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.Mvc.Authorization
{
    public class AppAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public ILogger Logger { get; set; }
 
        public AppAuthorizationFilter(ILogger<AppAuthorizationFilter> logger)
 
        {
 
            Logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
 
            await Task.Run(()=> { });
        }
    }
}
