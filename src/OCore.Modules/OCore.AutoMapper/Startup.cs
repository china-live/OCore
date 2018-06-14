using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using OCore.Modules;

namespace OCore.AutoMapper
{
    public class Startup : StartupBase
    {
        private readonly IServiceProvider _applicationServices;

        public Startup(IServiceProvider applicationServices)
        {
            _applicationServices = applicationServices;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            Mapper.Reset();//模块被禁用、开启后可能会造成Mapper多次初始化引发InvalidOperationException异常，在初始化之前先重置一下确保安全

            services.AddAutoMapper();
        }
    }
}
