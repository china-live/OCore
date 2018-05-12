using Microsoft.Extensions.DependencyInjection;
using XCore.Scripting.Files;

namespace XCore.Scripting
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScripting(this IServiceCollection services)
        {
            services.AddSingleton<IScriptingManager, DefaultScriptingManager>();
            services.AddSingleton<IGlobalMethodProvider, CommonGeneratorMethods>();

            services.AddSingleton<IScriptingEngine, FilesScriptEngine>();
            return services;
        }
    }
}