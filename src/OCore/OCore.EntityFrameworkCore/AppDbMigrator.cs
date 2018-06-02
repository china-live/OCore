namespace OCore.EntityFrameworkCore
{
    public class AppDbMigrator {
        public string MigrationsAssembly { get; set; }
    }

    /*
     * 在整个应用程序为非多租户（单租户）的情况下，系统会生成一个默认的ShellSettings实例。
     * 但是该实例只有一个名称和运行状态，是没有数据库类型、连接字符串、表前缀等配置（多租户模式时这些都从tenants.json或settings.txt里配置）。
     * 这时候可以在DI中注册一个单例的AppDbContextOptions实例来传递到AppDbContext中对默认的ShellSettings实例进行补充。
     */
    public class AppDbContextOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseProvider { get; set; }
        public string TablePrefix { get; set; }
    }
}
