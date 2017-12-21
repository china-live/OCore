using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Article.EntityFrameworkCore
{
    public class ArticleEntityTypeConfiguration: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> a)
        {
            a.ToTable("XCore_Articles");
            a.HasKey(r => r.Id);
            a.Ignore(r => r.Properties);

            a.Property(r => r.Title).HasMaxLength(256);
            a.Property(r => r.Source).HasMaxLength(256);
        }
    }

    public class TencentVodEntityTypeConfiguration : IEntityTypeConfiguration<TencentVod>
    {
        public void Configure(EntityTypeBuilder<TencentVod> a)
        {
            a.ToTable("XCore_TencentVods");
            a.HasKey(r => r.Id);
            a.Ignore(r => r.Properties);

            a.Property(r => r.Title).HasMaxLength(256);
        }
    }
}
