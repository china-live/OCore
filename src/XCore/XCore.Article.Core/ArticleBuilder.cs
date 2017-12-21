using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Article
{
    public static class ArticleServiceCollectionExtensions
    {
        public static ArticleBuilder AddActicle<TActicle>(this IServiceCollection services) where TActicle : class
        {
            services.TryAddScoped<IArticleValidator<TActicle>, ArticleValidator<TActicle>>();
            services.TryAddScoped<ArticleManager<TActicle>, ArticleManager<TActicle>>();


            return new ArticleBuilder(typeof(TActicle),  services);
        }  
    }

    public class ArticleBuilder
    {
        public ArticleBuilder(Type article, IServiceCollection services)
        {
            ArticleType = article;
            Services = services;
        }
        public Type ArticleType { get; private set; }

        public IServiceCollection Services { get; private set; }

        public virtual ArticleBuilder AddArticleStore<TUser>() where TUser : class
            => AddScoped(typeof(IArticleStore<>).MakeGenericType(ArticleType), typeof(TUser));

        private ArticleBuilder AddScoped(Type serviceType, Type concreteType)
        {
            Services.AddScoped(serviceType, concreteType);
            return this;
        }
    }


}
