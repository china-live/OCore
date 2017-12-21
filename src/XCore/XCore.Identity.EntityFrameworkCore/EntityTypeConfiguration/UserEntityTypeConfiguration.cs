using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace XCore.Identity.EntityFrameworkCore
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable("XCore_Users");
            b.HasKey(u => u.Id);
            b.Ignore(u => u.Properties);
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);
            b.Property(u => u.FullName).HasMaxLength(256);

            // Replace with b.HasMany<IdentityUserClaim>().
            b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> b)
        {
            b.ToTable("XCore_UserClaims");
            b.Ignore(u => u.Properties);
            b.HasKey(u => u.Id);
        }
    }
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> b)
        {
            b.ToTable("XCore_UserLogins");
            b.Ignore(u => u.Properties);
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
        }
    }
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> b)
        {
            b.ToTable("XCore_UserTokens");
            b.Ignore(u => u.Properties);
            b.HasKey(l => new { l.UserId, l.LoginProvider, l.Name });
        }
    }

    //public static class EntityTypeConfiguration
    //{
    //    public static void Init()
    //    {

    //    }
    //}
}
