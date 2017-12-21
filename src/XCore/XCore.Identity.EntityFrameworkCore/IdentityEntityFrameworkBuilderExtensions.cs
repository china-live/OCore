using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.EntityFrameworkCore;

namespace XCore.Identity.EntityFrameworkCore
{
    public static class IdentityEntityFrameworkBuilderExtensions
    {
        public static IdentityBuilder AddEntityFrameworkStores(this IdentityBuilder builder)
        {
            //AddStores(builder.Services, builder.UserType, builder.RoleType);

            builder.AddUserStore<UserStore>();
            builder.AddRoleStore<RoleStore>();
            return builder;
        }

        //private static void AddStores(IServiceCollection services, Type userType, Type roleType)
        //{
        //    services.AddScoped<IUserStore<User>, UserStore>();
        //    services.AddScoped<IRoleStore<Role>, RoleStore>();
        //}
    }
}