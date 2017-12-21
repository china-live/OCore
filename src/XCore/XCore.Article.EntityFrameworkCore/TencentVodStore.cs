using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;
using XCore.EntityFrameworkCore;
using System.Linq;

namespace XCore.Article.EntityFrameworkCore
{
    public class TencentVodStore : ITencentVodStore<TencentVod>, IQueryableTencentVodStore<TencentVod>
    {
        public TencentVodStore(AppDbContext context, TencentVodErrorDescriber describer = null)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ErrorDescriber = new TencentVodErrorDescriber();
        }

        private bool _disposed;
        private DbSet<TencentVod> TencentVodSet { get { return Context.Set<TencentVod>(); } }
        public DbContext Context { get; private set; }
        public TencentVodErrorDescriber ErrorDescriber { get; set; }
        public bool AutoSaveChanges { get; set; } = true;

        public virtual IQueryable<TencentVod> TencentVods
        {
            get { return TencentVodSet; }
        }


        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }

        //public virtual int ConvertIdFromString(int id)
        //{
        //    return (int)TypeDescriptor.GetConverter(typeof(int)).ConvertFrom(id);
        //}

        public virtual async Task<DtoResult> CreateAsync(TencentVod vod, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (vod == null)
            {
                throw new ArgumentNullException(nameof(vod));
            }
            Context.Add(vod);
            await SaveChanges(cancellationToken);
            return DtoResult.Success;
        }

        public virtual async Task<DtoResult> DeleteAsync(TencentVod vod, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (vod == null)
            {
                throw new ArgumentNullException(nameof(vod));
            }

            Context.Remove(vod);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return DtoResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }
            return DtoResult.Success;
        }



        public virtual Task<TencentVod> FindByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            //var articleId = ConvertIdFromString(userId);
            return TencentVodSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<DtoResult> UpdateAsync(TencentVod article, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            Context.Attach(article);
            //article.ConcurrencyStamp = Guid.NewGuid().ToString();
            Context.Update(article);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return DtoResult.Failed(ErrorDescriber.ConcurrencyFailure());
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
