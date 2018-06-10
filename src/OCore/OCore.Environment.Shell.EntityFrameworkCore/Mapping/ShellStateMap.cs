using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCore.EntityFrameworkCore;
using OCore.Environment.Shell.State;

namespace OCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellStateMap : IEntityTypeConfiguration<ShellState>
    {
        public void Configure(EntityTypeBuilder<ShellState> a)
        {
            a.ToTable("ShellState");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);
        }
    }

    public class ShellFeatureStateMap : IEntityTypeConfiguration<ShellFeatureState>
    {
        public void Configure(EntityTypeBuilder<ShellFeatureState> a)
        {
            a.ToTable("ShellFeatureState");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);
            a.Property(e => e.InstallState);
            a.Property(e => e.EnableState);
            //a.HasConversion(converter);
        }
    }
}
