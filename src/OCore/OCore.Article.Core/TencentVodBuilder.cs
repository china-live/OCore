using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace OCore.Article
{
    public class TencentVodBuilder
    {
        public TencentVodBuilder(Type tencentVod, IServiceCollection services)
        {
            TencentVodType = tencentVod;
            Services = services;
        }
        public Type TencentVodType { get; private set; }

        public IServiceCollection Services { get; private set; }

        public virtual TencentVodBuilder AddTencentVodStore<TUser>() where TUser : class
            => AddScoped(typeof(ITencentVodStore<>).MakeGenericType(TencentVodType), typeof(TUser));

        private TencentVodBuilder AddScoped(Type serviceType, Type concreteType)
        {
            Services.AddScoped(serviceType, concreteType);
            return this;
        }
    }

    public static class TencentVodServiceCollectionExtensions
    {
        public static TencentVodBuilder AddTencentVod<TTencentVod>(this IServiceCollection services) where TTencentVod : class
        {
            services.TryAddScoped<ITencentVodValidator<TTencentVod>, TencentVodValidator<TTencentVod>>();
            services.TryAddScoped<TencentVodManager<TTencentVod>, TencentVodManager<TTencentVod>>();


            return new TencentVodBuilder(typeof(TTencentVod), services);
        }
    }
}
