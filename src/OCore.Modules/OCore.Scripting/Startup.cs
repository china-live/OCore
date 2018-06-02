using OCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using OCore.Scripting.JavaScript;

namespace OCore.Scripting
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScripting();
            services.AddJavaScriptEngine();
        }
    }
}
