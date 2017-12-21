using System;
using Microsoft.Extensions.DependencyInjection;

namespace XCore.Modules
{
    /// <summary>
    /// 定义模块的服务集
    /// </summary>
    public class ModularServiceCollection
    {
        private IServiceCollection _services;

        public ModularServiceCollection(IServiceCollection services)
        {
            _services = services;
        }

        public ModularServiceCollection Configure(Action<IServiceCollection> services)
        {
            services(_services);
            return this;
        }
    }
}
