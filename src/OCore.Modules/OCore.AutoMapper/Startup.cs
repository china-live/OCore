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
            services.AddAutoMapper();
        }
    }
}
