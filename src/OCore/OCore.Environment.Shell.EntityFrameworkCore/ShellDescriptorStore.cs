using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OCore.Common;
using OCore.EntityFrameworkCore;
using OCore.Environment.Shell.Data;
using OCore.Environment.Shell.Descriptor.Models;

namespace OCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellDescriptorStore : IShellDescriptorStore<ShellDescriptor>
    {
        public ShellDescriptorStore(AppDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private bool _disposed;
        public AppDbContext Context { get; private set; }

        private DbSet<ShellDescriptor> ShellDescriptorSet { get { return Context.Set<ShellDescriptor>(); } }
        private DbSet<ShellFeature> ShellFeatureSet { get { return Context.Set<ShellFeature>(); } }
        private DbSet<ShellParameter> ShellParameterSet { get { return Context.Set<ShellParameter>(); } }

        public bool AutoSaveChanges { get; set; } = true;

        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }

        private IQueryable<ShellDescriptor> ShellDescriptorsNoTracking
        {
            get
            {
                return ShellDescriptorSet.Include(c => c.Features).Include(c => c.Parameters).AsNoTracking();/**/
            }
        }
        private IQueryable<ShellDescriptor> ShellDescriptors
        {
            get
            {
                return ShellDescriptorSet.Include(c => c.Features).Include(c => c.Parameters);/**/
            }
        }

        public async Task<ShellDescriptor> GetShellDescriptorAsync()
        {
            return await ShellDescriptorsNoTracking.FirstOrDefaultAsync();
        }

        public async Task<ShellDescriptor> CreateAsync(IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            enabledFeatures = enabledFeatures ?? new List<ShellFeature>();
            parameters = parameters ?? new List<ShellParameter>();

            var shellDescriptor = new ShellDescriptor { SerialNumber = 1 };
            shellDescriptor.Features = enabledFeatures.ToList();
            shellDescriptor.Parameters = parameters.ToList();
            await ShellDescriptorSet.AddAsync(shellDescriptor);

            await SaveChanges(cancellationToken);
            return shellDescriptor;
        }

        public async Task<ShellDescriptor> UpdateAsync(int shellDescriptorId, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            enabledFeatures = enabledFeatures ?? new List<ShellFeature>();
            parameters = parameters ?? new List<ShellParameter>();

            var model = await ShellDescriptors.FirstOrDefaultAsync(c => c.Id == shellDescriptorId);

            ShellFeatureSet.RemoveRange(model.Features);
            ShellParameterSet.RemoveRange(model.Parameters);
            await SaveChanges(cancellationToken);

            model.SerialNumber++;
            model.Features = enabledFeatures.ToList();
            model.Parameters = parameters.ToList();
            ShellDescriptorSet.Update(model);
            await SaveChanges(cancellationToken);
            return model;
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