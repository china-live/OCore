using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;

namespace XCore.Environment.Shell.Data
{
    public interface IShellStateStore<T> : IDisposable where T : class
    {
        Task<DtoResult> CreateAsync(T shellState, CancellationToken cancellationToken);
        Task<DtoResult> UpdateAsync(T shellState, CancellationToken cancellationToken);
    }
    public interface IQueryableShellStateStore<T> : IShellStateStore<T> where T : class
    {
        IQueryable<T> ShellStates { get; }
    }
}
