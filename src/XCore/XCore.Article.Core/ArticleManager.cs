using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace XCore.Article
{
    public class ArticleManager<TArticle> : IDisposable where TArticle : class
    {

        protected internal IArticleStore<TArticle> Store { get; set; }
        public IList<IArticleValidator<TArticle>> ArticleValidators { get; } = new List<IArticleValidator<TArticle>>();

        public virtual ILogger Logger { get; set; }

        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public ArticleManager(IArticleStore<TArticle> store,
            IEnumerable<IArticleValidator<TArticle>> articleValidators,
            ILogger<ArticleManager<TArticle>> logger)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            Store = store;
            if (articleValidators != null)
            {
                foreach (var v in articleValidators)
                {
                    ArticleValidators.Add(v);
                }
            }
        }

        public virtual IQueryable<TArticle> Articles
        {
            get
            {
                var queryableStore = Store as IQueryableArticleStore<TArticle>;
                if (queryableStore == null)
                {
                    throw new NotSupportedException("没有找到IQueryableArticleStore的实例");
                }
                return queryableStore.Articles;
            }
        }

        public virtual bool SupportsQueryableArticles
        {
            get
            {
                ThrowIfDisposed();
                return Store is IQueryableArticleStore<TArticle>;
            }
        }
        public virtual Task<TArticle> FindByIdAsync(int id)
        {
            ThrowIfDisposed();
            return Store.FindByIdAsync(id, CancellationToken);
        }

        public virtual async Task<DtoResult> CreateAsync(TArticle article)
        {
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }
            var result = await ValidateArticleInternal(article);
            if (!result.Succeeded)
            {
                return result;
            }
            result = await Store.CreateAsync(article, CancellationToken);
            return result;
        }

        public virtual Task<DtoResult> UpdateAsync(TArticle article)
        {
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            return UpdateArticleAsync(article);
        }
        public virtual Task<DtoResult> DeleteAsync(TArticle article)
        {
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            return Store.DeleteAsync(article, CancellationToken);
        }

        private async Task<DtoResult> UpdateArticleAsync(TArticle article)
        {
            var result = await ValidateArticleInternal(article);
            if (!result.Succeeded)
            {
                return result;
            }

            return await Store.UpdateAsync(article, CancellationToken);
        }

        private async Task<DtoResult> ValidateArticleInternal(TArticle article)
        {
            var errors = new List<DtoError>();
            foreach (var v in ArticleValidators)
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
