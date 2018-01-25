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
            a.ToTable("XCore_ShellDescriptor");
            a.HasKey(r => r.Id);
        }
    }

    public class ShellStateEntityTypeConfiguration : IEntityTypeConfiguration<ShellState>
    {
        public void Configure(EntityTypeBuilder<ShellState> a)
        {
            a.ToTable("XCore_ShellState");
            a.HasKey(r => r.Id);
        }
    }
}
