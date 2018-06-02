using Microsoft.Extensions.DependencyInjection;
using System;

namespace OCore.Article.EntityFrameworkCore
{



    public static class ArticleEntityFrameworkBuilderExtensions
    {
        public static ArticleBuilder AddEntityFrameworkStores(this ArticleBuilder builder)
        {
            //AddStores(builder.Services, builder.UserType, builder.RoleType);
            builder.AddArticleStore<ArticleStore>();
            return builder;
        }

        //private static void AddStores(IServiceCollection services, Type userType, Type roleType)
        //{
        //    services.AddScoped<IUserStore<User>, UserStore>();
        //    services.AddScoped<IRoleStore<Role>, RoleStore>();
        //}
    }
}
