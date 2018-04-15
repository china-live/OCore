using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Modules
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
