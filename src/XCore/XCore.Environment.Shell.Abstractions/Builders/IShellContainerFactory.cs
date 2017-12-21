using XCore.Environment.Shell.Builders.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Environment.Shell.Builders
{
    public interface IShellContainerFactory
    {
        IServiceProvider CreateContainer(ShellSettings settings, ShellBlueprint blueprint);
    }
}
