using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using XCore.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace XCore.HomeRoute
{
    public class Startup : StartupBase
    {
        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var inlineConstraintResolver = serviceProvider.GetService<IInlineConstraintResolver>();
            routes.Routes.Add(new HomePageRoute(routes, inlineConstraintResolver));
        }
    }
}
