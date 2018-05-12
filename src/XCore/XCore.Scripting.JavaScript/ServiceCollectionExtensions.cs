using Microsoft.Extensions.DependencyInjection;

namespace XCore.Scripting.JavaScript
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJavaScriptEngine(this IServiceCollection services)
        {
            services.AddSingleton<IScriptingEngine, JavaScriptEngine>();

            return services;
        }
    }
}