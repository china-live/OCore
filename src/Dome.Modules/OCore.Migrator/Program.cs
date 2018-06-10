using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using OCore.EntityFrameworkCore;
using OCore.Environment.Shell;
using OCore.Modules;

namespace OCore.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        //public static void Main(string[] args)
        //{
        //    //var host = BuildWebHost(args);

        //    ////using (var scope = host.Services.CreateScope())
        //    ////{
        //    ////    var services = scope.ServiceProvider;

        //    ////    try
        //    ////    {
        //    ////        IdentityServerDatabaseInitialization.InitializeDatabase(services);
        //    ////    }
        //    ////    catch (Exception ex)
        //    ////    {
        //    ////        var logger = services.GetRequiredService<ILogger<Program>>();
        //    ////        logger.LogError(ex, "An error occurred Initializing the DB.");
        //    ////    }
        //    ////}

        //    //host.Run();


        //    //var host = new Microsoft.AspNetCore.Hosting.WebHostBuilder()
        //    //    .UseKestrel()
        //    //    .UseContentRoot(Directory.GetCurrentDirectory())
        //    //    .UseIISIntegration()
        //    //    .UseStartup<Startup>()
        //    //    .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Trace))
        //    //    .Build();

        //    //host.Run();
        //}

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Trace))
        //        .UseNLogWeb()
        //        .Build();

    }

    public class Startup : StartupBase
    {

        public override void ConfigureServices(IServiceCollection services)
        {
            //var serviceProvider = services.BuildServiceProvider();
            //var defaultConnection = serviceProvider.GetService<IConfiguration>().GetConnectionString("DefaultConnection");
            //services.AddEntityFrameworkCore(/*defaultConnection*/);
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }
    }

    //    class MyDesignTimeServices : IDesignTimeServices
    //    {
    //        public void ConfigureDesignTimeServices(IServiceCollection services)
    //            => services.AddSingleton<IMigrationsCodeGenerator, MyMigrationsCodeGenerator>()
    //}

    public class MyDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            //var log = new LoggerFactory();
            //var logger = log.CreateLogger<AppDbContext>();
            //services.AddSingleton(logger);

            //services.AddSingleton(new ShellSettings()
            //{
            //    Name = "Default",
            //    State = Environment.Shell.Models.TenantState.Running,
            //    //DatabaseProvider = "SqlServer",
            //    //ConnectionString = connectionString,
            //    TablePrefix = "OCore"
            //});
        }
    }

    public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Startup>()
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("OCore.Migrator"));

            var entityManager = new MigrationEntityTypeProvider(new String[] { "OCore.Identity.EntityFrameworkCore",
                //"OCore.Article.EntityFrameworkCore",
                //"OCore.Environment.Shell.EntityFrameworkCore",
                //"OCore.Recipes",
                //"OCore.Settings" 
            });
 
            Assembly assembly = typeof(AppContextFactory).GetTypeInfo().Assembly;
            AssemblyName assemblyName = assembly.GetName();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IEntityTypeProvider>(entityManager);
            serviceCollection.AddSingleton(new ShellSettings()
            {
                Name = "Default",
                State = Environment.Shell.Models.TenantState.Running,
                //DatabaseProvider = "SqlServer",
                //ConnectionString = connectionString,
                //TablePrefix = "OCore"
            });
            serviceCollection.AddSingleton(new AppDbContextOptions() { ConnectionString = connectionString, DatabaseProvider = "SqlServer"/*, TablePrefix = "OCore" */});
            serviceCollection.AddSingleton(new AppDbMigrator() { MigrationsAssembly = assemblyName.Name });
            //var log = new LoggerFactory();
            //var logger = log.CreateLogger<AppDbContext>();
            //serviceCollection.AddSingleton(logger);
            var services = serviceCollection.BuildServiceProvider();


            return new AppDbContext(services);
        }
    }
}
