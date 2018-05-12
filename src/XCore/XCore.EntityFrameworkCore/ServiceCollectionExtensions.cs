using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.Environment.Shell;

namespace XCore.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services,string connectionString)
        //{
        //    services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        //    //services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(connectionString));

        //    services.AddSingleton<IEntityManager,DefaultEntityManager>();

        //    services.AddSingleton<DbContext,AppDbContext>();

        //    return services;
        //}

        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services)
        {

            //services.AddDbContext<AppDbContext>((a, b) =>
            //{
            //    var shellSettions = a.GetService<ShellSettings>();
            //    b.UseSqlServer(shellSettions.ConnectionString);
            //});
            services.AddDbContext<AppDbContext>();

            services.AddSingleton<IEntityManager, DefaultEntityManager>();

            //services.AddSingleton<DbContext,AppDbContext>();

            return services;
        }

        //private string GetConnectionString() {
        //    var shellSettings = sp.GetService<ShellSettings>();
        //}
    }
}
