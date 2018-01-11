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
            if (_shellState != null)
            {
                return _shellState;
            }

            _shellState = ShellStates.FirstOrDefault();

            await CreateOrUpdateShellState();

            return _shellState;
        }

        public async Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature {0} EnableState changed from {1} to {2}",
                             featureState.Id, featureState.EnableState, value);
            }

            var previousFeatureState = await GetOrCreateFeatureStateAsync(featureState.Id);
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

            await CreateOrUpdateShellState();
        }

        public async Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature {0} InstallState changed from {1} to {2}", featureState.Id, featureState.InstallState, value);
            }

            var previousFeatureState = await GetOrCreateFeatureStateAsync(featureState.Id);
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

            await CreateOrUpdateShellState();
        }

        private async Task<ShellFeatureState> GetOrCreateFeatureStateAsync(string id)
        {
            var shellState = await GetShellStateAsync();
            var featureState = shellState.Features.FirstOrDefault(x => x.Id == id);

            if (featureState == null)
            {
                featureState = new ShellFeatureState() { Id = id };
                _shellState.Features.Add(featureState);
            }

            return featureState;
        }

        private async Task CreateOrUpdateShellState()
        {
            if (_shellState == null)
            {
                _shellState = new ShellState();
                await Store.CreateAsync(_shellState, CancellationToken);
            }
            else {
                await Store.UpdateAsync(_shellState, CancellationToken);
            }
        }

        private IQueryable<ShellState> ShellStates
        {
            get
            {
                var queryableStore = Store as IQueryableShellStateStore<ShellState>;
                if (queryableStore == null)
                {
                    throw new NotSupportedException("没有找到IQueryableShellStateStore的实现");
                }
                return queryableStore.ShellStates;
            }
        }
    }
}
