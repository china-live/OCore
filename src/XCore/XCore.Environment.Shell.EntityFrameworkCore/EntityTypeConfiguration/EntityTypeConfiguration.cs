using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Environment.Shell.State;

namespace XCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellDescriptorEntityTypeConfiguration : IEntityTypeConfiguration<ShellDescriptor>
    {
        public void Configure(EntityTypeBuilder<ShellDescriptor> a)
        {
            a.ToTable("ShellDescriptor");
            a.HasKey(r => r.Id);
        }
    }
    public class ShellFeatureEntityTypeConfiguration : IEntityTypeConfiguration<ShellFeature>
    {
        public void Configure(EntityTypeBuilder<ShellFeature> a)
        {
            a.ToTable("ShellFeature");
            a.HasKey(r => r.Id);
            a.Property(b => b.Id).ValueGeneratedNever();
        }
    }
    public class ShellParameterEntityTypeConfiguration : IEntityTypeConfiguration<ShellParameter>
    {
        public void Configure(EntityTypeBuilder<ShellParameter> a)
        {
            a.ToTable("ShellParameter");
            a.HasKey(r => new { r.Name,r.Value,r.Component});
        }
    }

    public class ShellStateEntityTypeConfiguration : IEntityTypeConfiguration<ShellState>
    {
        public void Configure(EntityTypeBuilder<ShellState> a)
        {
            a.ToTable("ShellState");
            a.HasKey(r => r.Id);
        }
    }
    public class ShellFeatureStateEntityTypeConfiguration : IEntityTypeConfiguration<ShellFeatureState>
    {
        public void Configure(EntityTypeBuilder<ShellFeatureState> a)
        {
            a.ToTable("ShellFeatureState");
            a.HasKey(r => r.Id);
            a.Property(e => e.InstallState);
            a.Property(e => e.EnableState);
            //a.HasConversion(converter);
        }
    }
}
