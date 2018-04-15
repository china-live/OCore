using System;
using XCore.Environment.Shell.Data;

namespace XCore.Environment.Shell.EntityFrameworkCore
{
    public static class EntityFrameworkBuilderExtensions
    {
        public static ShellBuilder AddEntityFrameworkStores(this ShellBuilder builder)
        {
            builder.AddShellDescriptorStore<ShellDescriptorStore>();
            builder.AddShellStateStore<ShellStateStore>();
            return builder;
        }
    }
}
