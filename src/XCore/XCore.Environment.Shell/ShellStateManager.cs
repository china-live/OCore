using XCore.Environment.Shell.State;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace XCore.Environment.Shell
{
    public class NullShellStateManager : IShellStateManager
    {
        public NullShellStateManager(ILogger<NullShellStateManager> logger)
        {
            Logger = logger;
        }

        ILogger Logger { get; set; }

        public Task<ShellState> GetShellStateAsync()
        {
            return Task.FromResult(new ShellState());
        }

        public Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature {0} EnableState changed from {1} to {2}",
                             featureState.Id, featureState.EnableState, value);
            }

            return Task.CompletedTask;
        }

        public Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature {0} InstallState changed from {1} to {2}", featureState.Id, featureState.InstallState, value);
            }

            return Task.CompletedTask;
        }
    }
}
