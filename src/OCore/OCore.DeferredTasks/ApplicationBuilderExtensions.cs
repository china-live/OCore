using Microsoft.AspNetCore.Builder;

namespace OCore.DeferredTasks
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddDeferredTasks(this IApplicationBuilder app)
        {
            app.UseMiddleware<DeferredTaskMiddleware>();

            return app;
        }
    }
}
