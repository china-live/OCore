using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using XCore.Environment.Shell;

namespace XCore.EntityFrameworkCore
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext(/*DbContextOptions<AppDbContext>*/DbContextOptions options, IEntityManager entityManager)
        //    : base(options)
        //{
        //    this.entityManager = entityManager;
        //}

        private readonly ShellSettings _shellSettings;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEntityManager _entityManager;

        public AppDbContext(/*DbContextOptions<AppDbContext> options,*/ IServiceProvider serviceProvider)//: base(options)
        {
            _serviceProvider = serviceProvider;
            _shellSettings = _serviceProvider.GetRequiredService<ShellSettings>();
            _entityManager = _serviceProvider.GetRequiredService<IEntityManager>();
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var type in _entityManager.GetEntitys())
            {
                if (builder.Model.FindEntityType(type) == null)//防止重复附加模型，否则会在生成指令中报错
                {
                    builder.Model.AddEntityType(type);
                }
            }

            foreach (var type in _entityManager.GetEntityTypeConfiguration())
            {
                dynamic mappingInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(mappingInstance);
            }

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseOrchardCore(_shellSettings);
            optionsBuilder.UseSqlServer(_shellSettings.ConnectionString);
        }
    }

    public static class OrchardEntityFrameworkExtensions
    {
        public static DbContextOptionsBuilder UseOrchardCore(this DbContextOptionsBuilder builder, ShellSettings settings)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            builder.ReplaceService<IModelCustomizer, OrchardModelCustomizer>();
            builder.ReplaceService<IModelCacheKeyFactory, OrchardModelCacheKeyFactory>();

            if (string.IsNullOrEmpty(settings.DatabaseProvider))
            {
                return builder;
            }

            switch (settings.DatabaseProvider)
            {
                case "SqlServer":
                    builder.UseSqlServer(settings.ConnectionString/*, options =>
                    {
                        if (!string.IsNullOrEmpty(settings.TablePrefix))
                        {
                            options.MigrationsHistoryTable($"{settings.TablePrefix}{HistoryRepository.DefaultTableName}");
                        }
                    }*/);
                    break;

                // Add other providers (Sqlite, MySQL w/ Pomelo, etc.)

                default:
                    throw new InvalidOperationException($"The specified database provider is not supported: {settings.DatabaseProvider}.");
            }

            return builder;
        }

        private class OrchardModelCacheKeyFactory : IModelCacheKeyFactory
        {
            //public object Create(DbContext context) => (context.GetType(), context.GetService<ShellSettings>().Name);

            public object Create(DbContext context)
            {
                return (context.GetType(), context.GetService<ShellSettings>().Name);
            }
        }

        private class OrchardModelCustomizer : RelationalModelCustomizer
        {
            public OrchardModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
            {
            }

            public override void Customize(ModelBuilder builder, DbContext context)
            {
                base.Customize(builder, context);

                var prefix = context.GetService<ShellSettings>().TablePrefix;
                if (string.IsNullOrEmpty(prefix))
                {
                    return;
                }

                foreach (var type in builder.Model.GetEntityTypes())
                {
                    type.Relational().TableName = $"{prefix}_{type.Relational().TableName}";
                }

                //var sp = (IInfrastructure<IServiceProvider>)context;
                //var dbOptions = sp.Instance.GetServices<DbContextOptions>();
                //foreach (var item in dbOptions)
                //{
                //    if (item.ContextType == dbContext.GetType())
                //        ConfigureDbContextEntityService.Configure(modelBuilder, item, dbContext);
                //}
            }
        }
    }
}
