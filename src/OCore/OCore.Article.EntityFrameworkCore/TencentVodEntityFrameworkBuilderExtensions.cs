namespace OCore.Article.EntityFrameworkCore
{
    public static class TencentVodEntityFrameworkBuilderExtensions
    {
        public static TencentVodBuilder AddEntityFrameworkStores(this TencentVodBuilder builder)
        {
            builder.AddTencentVodStore<TencentVodStore>();
            return builder;
        }
    }
}
