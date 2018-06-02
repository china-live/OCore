using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OCore.Common;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace OCore.Article
{
    public class TencentVodManager<TVod> : IDisposable where TVod : class
    {

        protected internal ITencentVodStore<TVod> Store { get; set; }
        public IList<ITencentVodValidator<TVod>> TencentVodValidators { get; } = new List<ITencentVodValidator<TVod>>();

        public virtual ILogger Logger { get; set; }

        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public TencentVodManager(ITencentVodStore<TVod> store,
            IEnumerable<ITencentVodValidator<TVod>> vodValidators,
            ILogger<TencentVodManager<TVod>> logger)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            Store = store;
            if (vodValidators != null)
            {
                foreach (var v in vodValidators)
                {
                    TencentVodValidators.Add(v);
                }
            }
        }

        public virtual IQueryable<TVod> TencentVods
        {
            get
            {
                var queryableStore = Store as IQueryableTencentVodStore<TVod>;
                if (queryableStore == null)
                {
                    throw new NotSupportedException("没有找到IQueryableArticleStore的实例");
                }
                return queryableStore.TencentVods;
            }
        }

        public virtual bool SupportsQueryableTencentVods
        {
            get
            {
                ThrowIfDisposed();
                return Store is IQueryableTencentVodStore<TVod>;
            }
        }
        public virtual Task<TVod> FindByIdAsync(int id)
        {
            ThrowIfDisposed();
            return Store.FindByIdAsync(id, CancellationToken);
        }

        public virtual async Task<DtoResult> CreateAsync(TVod article)
        {
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }
            var result = await ValidateTencentVodInternal(article);
            if (!result.Succeeded)
            {
                return result;
            }
            result = await Store.CreateAsync(article, CancellationToken);
            return result;
        }

        public virtual Task<DtoResult> UpdateAsync(TVod article)
        {
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            return UpdateTencentVodAsync(article);
        }
        public virtual Task<DtoResult> DeleteAsync(TVod article)
        {
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            return Store.DeleteAsync(article, CancellationToken);
        }

        private async Task<DtoResult> UpdateTencentVodAsync(TVod vod)
        {
            var result = await ValidateTencentVodInternal(vod);
            if (!result.Succeeded)
            {
                return result;
            }

            return await Store.UpdateAsync(vod, CancellationToken);
        }

        private async Task<DtoResult> ValidateTencentVodInternal(TVod article)
        {
            var errors = new List<DtoError>();
            foreach (var v in TencentVodValidators)
            {
                var result = await v.ValidateAsync(this, article);
                if (!result.Succeeded)
                {
                    errors.AddRange(result.Errors);
                }
            }
            if (errors.Count > 0)
            {
                //Logger.LogWarning(0, "Role {roleId} validation failed: {errors}.", await GetRoleIdAsync(role), string.Join(";", errors.Select(e => e.Code)));
                return DtoResult.Failed(errors.ToArray());
            }
            return DtoResult.Success;
        }


        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Store.Dispose();
                _disposed = true;
            }
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
