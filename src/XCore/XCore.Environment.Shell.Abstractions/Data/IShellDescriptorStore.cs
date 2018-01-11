using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;

namespace XCore.Environment.Shell.Data
{
    public interface IShellDescriptorStore<T> : IDisposable where T : class
    {
        Task<DtoResult> CreateAsync(T shellDescriptor, CancellationToken cancellationToken);
        Task<DtoResult> UpdateAsync(T shellDescriptor, CancellationToken cancellationToken);
    }


    public interface IQueryableShellDescriptorStore<T> : IShellDescriptorStore<T> where T : class
    {
        IQueryable<T> ShellDescriptors { get; }
    }
}
