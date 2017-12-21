using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using XCore.EntityFrameworkCore;
using XCore.Modules;

namespace XCore.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Startup : StartupBase
    {

        public override void ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var defaultConnection = serviceProvider.GetService<IConfiguration>().GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkCore(defaultConnection);
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }
    }

    public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        //string conString = "Data Source=www.dming.top;Database=XCore;UID=sa;PWD=xtcser@14582.net;MultipleActiveResultSets=true;";
        //string conString = "Data Source=192.168.8.127;Database=TongGenTongMeng;UID=Tong;PWD=123;MultipleActiveResultSets=true;";

        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Startup>()
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("XCore.Migrator"));

            var entityManager = new MigrationEntityManager(); //MigrationEntityManager实现了IEntityManager接口
            entityManager.LoadAssemblys("XCore.Identity.EntityFrameworkCore");//加载实现了IEntityTypeConfiguration接口的类所在的程序集
            entityManager.LoadAssemblys("XCore.Article.EntityFrameworkCore");

            return new AppDbContext(optionsBuilder.Options, entityManager);
        }
    }
}
