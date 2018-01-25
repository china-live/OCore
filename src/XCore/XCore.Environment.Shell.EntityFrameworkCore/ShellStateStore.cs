using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;
using XCore.EntityFrameworkCore;
using XCore.Environment.Shell.Data;
using XCore.Environment.Shell.State;

namespace XCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellStateStore : IShellStateStore<ShellState>
    {
        public ShellStateStore(AppDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private bool _disposed;
        public DbContext Context { get; private set; }

        private DbSet<ShellState> ShellStateSet { get { return Context.Set<ShellState>(); } }

        public bool AutoSaveChanges { get; set; } = true;

        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }


        public async Task<DtoResult> CreateAsync(ShellState shellState, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (shellState == null)
            {
                throw new ArgumentNullException(nameof(shellState));
            }
            Context.Add(shellState);
            await SaveChanges(cancellationToken);
            return DtoResult.Success;
        }

        public async Task<DtoResult> UpdateAsync(ShellState shellState, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (shellState == null)
            {
                throw new ArgumentNullException(nameof(shellState));
            }

            Context.Attach(shellState);
            Context.Update(shellState);
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