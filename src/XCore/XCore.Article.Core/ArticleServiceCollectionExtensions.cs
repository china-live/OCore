using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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


}
