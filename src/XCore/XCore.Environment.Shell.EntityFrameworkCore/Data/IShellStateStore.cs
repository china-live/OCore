using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;
using XCore.Environment.Shell.State;

namespace XCore.Environment.Shell.Data
{
    public interface IShellStateStore<T> : IDisposable where T : class
    {
        Task<ShellState> GetOrCreateAsync(CancellationToken cancellationToken);
        Task<ShellFeatureState> GetOrCreateFeatureStateAsync(string id, CancellationToken cancellationToken);
        Task<DtoResult> UpdateFeatureStateAsync(ShellFeatureState featureState, CancellationToken cancellationToken);
    }
    public interface IQueryableShellStateStore<T> : IShellStateStore<T> where T : class
    {
        IQueryable<T> ShellStates { get; }
    }
}
