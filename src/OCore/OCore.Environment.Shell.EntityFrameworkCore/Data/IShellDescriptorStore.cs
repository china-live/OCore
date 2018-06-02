using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OCore.Common;
using OCore.Environment.Shell.Descriptor.Models;

namespace OCore.Environment.Shell.Data
{
    public interface IShellDescriptorStore<T> : IDisposable where T : class
    {
        Task<ShellDescriptor> GetShellDescriptorAsync();
        Task<ShellDescriptor> CreateAsync(IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters, CancellationToken cancellationToken);
        Task<ShellDescriptor> UpdateAsync(int shellDescriptorId, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters, CancellationToken cancellationToken);
    }


    //public interface IQueryableShellDescriptorStore<T> : IShellDescriptorStore<T> where T : class
    //{
    //    IQueryable<T> ShellDescriptors { get; }
    //}
}
