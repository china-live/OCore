using Microsoft.AspNetCore.Hosting;

namespace OCore.ResourceManagement
{
    public class DefaultRequireSettingsProvider : IRequireSettingsProvider
    {
        private readonly IHostingEnvironment _env;

        public DefaultRequireSettingsProvider(IHostingEnvironment env)
        {
            _env = env;
        }

        public RequireSettings GetDefault()
        {
            return new RequireSettings
            {
                DebugMode = _env.IsDevelopment(),
                CdnMode = !_env.IsDevelopment(),
                Culture = "",
            };
        }
    }
}
