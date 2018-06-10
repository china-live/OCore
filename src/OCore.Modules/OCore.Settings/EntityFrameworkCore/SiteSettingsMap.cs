using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCore.EntityFrameworkCore;

namespace OCore.Settings.EntityFrameworkCore
{
    public class SiteSettingsMap : IEntityTypeConfiguration<SiteSettingsEntity>
    {
        public void Configure(EntityTypeBuilder<SiteSettingsEntity> a)
        {
            a.ToTable("SiteSettings");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);
            a.Ignore(r => r.HomeRoute);
        }
    }
}
