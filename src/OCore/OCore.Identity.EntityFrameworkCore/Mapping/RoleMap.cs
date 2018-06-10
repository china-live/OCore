using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCore.EntityFrameworkCore;

namespace OCore.Identity.EntityFrameworkCore
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {
            b.ToTable("OCore_Roles");
            b.HasKey(r => r.Id);
            //b.Ignore(r => r.Properties);
            b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(r => r.Name).HasMaxLength(256);
            b.Property(r => r.NormalizedName).HasMaxLength(256);

            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            b.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        }
    }
    public class RoleClaimMap : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> b)
        {
            b.ToTable("OCore_RoleClaims");
            //b.Ignore(r => r.Properties);
            b.HasKey(rc => rc.Id);
        }
    }
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> b)
        {
            b.ToTable("OCore_UserRoles");
            //b.Ignore(r => r.Properties);
            b.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}
