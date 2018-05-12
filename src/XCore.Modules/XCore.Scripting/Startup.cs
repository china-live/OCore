using XCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using XCore.Scripting.JavaScript;

namespace XCore.Scripting
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
