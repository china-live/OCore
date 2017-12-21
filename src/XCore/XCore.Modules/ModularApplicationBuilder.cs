using Microsoft.AspNetCore.Builder;
using System;

namespace XCore.Modules.Extensions
{
    public class ModularApplicationBuilder
    {
        private IApplicationBuilder _app;

        public ModularApplicationBuilder(IApplicationBuilder app)
        {
            _app = app;
        }

        public ModularApplicationBuilder Configure(Action<IApplicationBuilder> configuration)
        {
            configuration(_app);
            return this;
        }
    }
}
