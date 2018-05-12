using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XCore.Environment.Shell.State;

namespace XCore.Environment.Shell.Data
{
    /// <summary>
    /// Stores <see cref="ShellState"/> in the database. 
    /// </summary>
    public class ShellStateManager : IShellStateManager
    {
        private ShellState _shellState;
        protected internal IShellStateStore<ShellState> Store { get; set; }
        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public ShellStateManager(IShellStateStore<ShellState> store, ILogger<ShellStateManager> logger)
        {
            Store = store;
            Logger = logger;
        }

        ILogger Logger { get; set; }

        public async Task<ShellState> GetShellStateAsync()
        {
            _shellState = await Store.GetOrCreateAsync(CancellationToken);

            return _shellState;
        }

        public async Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature {0} EnableState changed from {1} to {2}",
                             featureState.Id, featureState.EnableState, value);
            }

            var previousFeatureState = await Store.GetOrCreateFeatureStateAsync(featureState.Id, CancellationToken);
            if (previousFeatureState.EnableState != featureState.EnableState)
            {
                if (Logger.IsEnabled(LogLevel.Warning))
                {
                    Logger.LogWarning("Feature {0} prior EnableState was {1} when {2} was expected",
                               featureState.Id, previousFeatureState.EnableState, featureState.EnableState);
                }
            }

            previousFeatureState.EnableState = value;
            featureState.EnableState = value;

            await Store.UpdateFeatureStateAsync(previousFeatureState,CancellationToken);
        }

        public async Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature {0} InstallState changed from {1} to {2}", featureState.Id, featureState.InstallState, value);
            }

            var previousFeatureState = await Store.GetOrCreateFeatureStateAsync(featureState.Id, CancellationToken);
            if (previousFeatureState.InstallState != featureState.InstallState)
            {
                if (Logger.IsEnabled(LogLevel.Warning))
                {
                    Logger.LogWarning("Feature {0} prior InstallState was {1} when {2} was expected",
                               featureState.Id, previousFeatureState.InstallState, featureState.InstallState);
                }
            }

            previousFeatureState.InstallState = value;
            featureState.InstallState = value;

            await Store.UpdateFeatureStateAsync(previousFeatureState, CancellationToken);
        }
    }
}
