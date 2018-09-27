using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using OCore.Environment.Shell;

namespace OCore.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 向系统注册使用EntityFrameworkCore作为数据持久化存储方案，
        /// 添加该项后会在内部维护/添加一个全局的（生命同期为单例）的数据库上下文实例（AppDbContext）,
        /// 所有注册到该上下文的实体的数据库操作都通过该上下文实现。
        /// 实体的发现是跟据EF的特点，通过加载IEntityTypeConfiguration<T>实例间接发现实体的，所以一定要为需要通过该AppDbContext操作的实体创建一个对应的IEntityTypeConfiguration<T>实例。
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="appDbContextOptions">用于提供非多租户时的情况下的一些数据库连接连接选项（多租户时有专门的配置文件tenants.json或通过程序录入方案settings.txt）</param>
        /// <param name="migrationsAssemblyName">设置用于该EF内维护的数据上下文（AppDbContext）的数据迁移的程序集名称</param>
        /// <returns></returns>
        public static OCoreBuilder AddEntityFrameworkCore(this OCoreBuilder builder, Action<AppDbContextOptions> appDbContextOptions = null, string migrationsAssemblyName = null) {
            builder.ConfigureServices(services => {
                //services.AddDbContext<AppDbContext>((a, b) =>
                //{
                //    var shellSettions = a.GetService<ShellSettings>();
                //    b.UseSqlServer(shellSettions.ConnectionString);
                //});
                var appDbContextOptins = new AppDbContextOptions();

                appDbContextOptions?.Invoke(appDbContextOptins);

                services.AddSingleton(appDbContextOptins);
                services.AddSingleton(new AppDbMigrator() { MigrationsAssembly = migrationsAssemblyName });
                services.AddSingleton<IEntityTypeProvider, DefaultEntityTypeProvider>();

                services.AddDbContext<AppDbContext>();
            });

            return builder;
        }
    }
}
