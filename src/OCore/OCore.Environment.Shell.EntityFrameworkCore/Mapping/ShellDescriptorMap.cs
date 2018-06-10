using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCore.EntityFrameworkCore;
using OCore.Environment.Shell.Descriptor.Models;
using OCore.Environment.Shell.State;

namespace OCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellDescriptorMap : IEntityTypeConfiguration<ShellDescriptor>
    {
        public void Configure(EntityTypeBuilder<ShellDescriptor> a)
        {
            a.ToTable("ShellDescriptor");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);
        }
    }
    public class ShellFeatureMap : IEntityTypeConfiguration<ShellFeature>
    {
        public void Configure(EntityTypeBuilder<ShellFeature> a)
        {
            a.ToTable("ShellFeature");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);
            a.Property(b => b.Id).ValueGeneratedNever();
        }
    }
    public class ShellParameterMap : IEntityTypeConfiguration<ShellParameter>
    {
        public void Configure(EntityTypeBuilder<ShellParameter> a)
        {
            a.ToTable("ShellParameter");
            a.Ignore(r => r.Id);
            //a.Ignore(r => r.Properties);
            a.HasKey(r => new { r.Name,r.Value,r.Component});
        }
    }
    
}
