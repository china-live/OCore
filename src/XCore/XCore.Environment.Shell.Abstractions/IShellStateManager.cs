using XCore.Environment.Shell.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XCore.Environment.Shell
{
    public interface IShellStateManager
    {
        Task<ShellState> GetShellStateAsync();
        Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value);
        Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value);
    }
}
