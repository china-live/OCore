using OCore.Environment.Shell.Builders.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCore.Environment.Shell.Builders
{
    public interface IShellContainerFactory
    {
        IServiceProvider CreateContainer(ShellSettings settings, ShellBlueprint blueprint);
    }
}
