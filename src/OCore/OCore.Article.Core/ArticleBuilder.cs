using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCore.Article
{

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
