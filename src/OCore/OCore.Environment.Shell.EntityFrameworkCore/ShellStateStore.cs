using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OCore.Common;
using OCore.EntityFrameworkCore;
using OCore.Environment.Shell.Data;
using OCore.Environment.Shell.State;

namespace OCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellStateStore : IShellStateStore<ShellState>, IQueryableShellStateStore<ShellState>
    {
        public ShellStateStore(AppDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private bool _disposed;
        public AppDbContext Context { get; private set; }

        private DbSet<ShellState> ShellStateSet { get { return Context.Set<ShellState>(); } }
        private DbSet<ShellFeatureState> ShellFeatureStateSet { get { return Context.Set<ShellFeatureState>(); } }

        public IQueryable<ShellState> ShellStates { get { return ShellStateSet.Include(c => c.Features); } }

        public IQueryable<ShellState> ShellStatesNoTracking { get { return ShellStateSet.Include(c => c.Features).AsNoTracking(); } }

        public IQueryable<ShellFeatureState> ShellFeatureStates { get { return ShellFeatureStateSet; } }

        public IQueryable<ShellFeatureState> ShellFeatureStatesNoTracking { get { return ShellFeatureStateSet.AsNoTracking(); } }

        public bool AutoSaveChanges { get; set; } = true;

        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }


        public async Task<ShellState> GetOrCreateAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
 
            var model = await ShellStates.OrderByDescending(c=>c.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                model = new ShellState();
                ShellStateSet.Add(model);
                await SaveChanges(cancellationToken);
            }
            return model;
        }

        public async Task<ShellFeatureState> GetOrCreateFeatureStateAsync(string id, CancellationToken cancellationToken)
        {
            var shellState = await GetOrCreateAsync(cancellationToken);
            var featureState = shellState.Features.FirstOrDefault(x => x.Id == id);

            if (featureState == null)
            {
                featureState = new ShellFeatureState() { Id = id};
                //ShellFeatureStateSet.Add(featureState);
                //await SaveChanges(cancellationToken);

                shellState.Features.Add(featureState);
                await SaveChanges(cancellationToken);
            }

            return featureState;
        }
 
        public async Task<DtoResult> UpdateFeatureStateAsync(ShellFeatureState featureState, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (featureState == null)
            {
                throw new ArgumentNullException(nameof(featureState));
            }

            var model = await ShellFeatureStates.SingleOrDefaultAsync(c=>c.Id == featureState.Id);
            model.EnableState = featureState.EnableState;
            model.InstallState = featureState.InstallState;
            Context.Update(model);
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