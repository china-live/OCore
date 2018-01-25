using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;
using XCore.EntityFrameworkCore;
using XCore.Environment.Shell.Data;
using XCore.Environment.Shell.Descriptor.Models;

namespace XCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellDescriptorStore : IShellDescriptorStore<ShellDescriptor>
    {
        public ShellDescriptorStore(AppDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private bool _disposed;
        public DbContext Context { get; private set; }

        private DbSet<ShellDescriptor> ShellDescriptorSet { get { return Context.Set<ShellDescriptor>(); } }

        public bool AutoSaveChanges { get; set; } = true;

        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }


        public async Task<DtoResult> CreateAsync(ShellDescriptor shellDescriptor, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (shellDescriptor == null)
            {
                throw new ArgumentNullException(nameof(shellDescriptor));
            }
            Context.Add(shellDescriptor);
            await SaveChanges(cancellationToken);
            return DtoResult.Success;
        }

        public async Task<DtoResult> UpdateAsync(ShellDescriptor shellDescriptor, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (shellDescriptor == null)
            {
                throw new ArgumentNullException(nameof(shellDescriptor));
            }

            Context.Attach(shellDescriptor);
            Context.Update(shellDescriptor);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return DtoResult.Failed(new DtoError
                {
                    Code = "ConcurrencyFailure",
                    Description = "并发故障"
                });
            }
            return DtoResult.Success;
        }

        public void Dispose()
        {
            _disposed = true;
        }
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}