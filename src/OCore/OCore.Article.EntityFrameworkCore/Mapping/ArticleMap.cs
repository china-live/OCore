using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCore.EntityFrameworkCore;

namespace OCore.Article.EntityFrameworkCore
{
    public class ArticleMap: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> a)
        {
            a.ToTable("OCore_Articles");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);

            a.Property(r => r.Title).HasMaxLength(256);
            a.Property(r => r.Source).HasMaxLength(256);
        }
    }

    public class TencentVodMap : IEntityTypeConfiguration<TencentVod>
    {
        public void Configure(EntityTypeBuilder<TencentVod> a)
        {
            a.ToTable("OCore_TencentVods");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);

            a.Property(r => r.Title).HasMaxLength(256);
        }
    }
}
