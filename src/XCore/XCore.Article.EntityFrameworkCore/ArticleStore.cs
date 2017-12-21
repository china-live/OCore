using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;
using XCore.EntityFrameworkCore;
using System.Linq;

namespace XCore.Article.EntityFrameworkCore
{
    public class ArticleStore : IArticleStore<Article>,IQueryableArticleStore<Article>
    {
        public ArticleStore(AppDbContext context, ArticleErrorDescriber describer = null)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ErrorDescriber = new ArticleErrorDescriber();
        }

        private bool _disposed;
        private DbSet<Article> ArticlesSet { get { return Context.Set<Article>(); } }
        public DbContext Context { get; private set; }
        public ArticleErrorDescriber  ErrorDescriber { get; set; }
        public bool AutoSaveChanges { get; set; } = true;
 
        public virtual IQueryable<Article> Articles
        {
            get { return ArticlesSet; }
        }


        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }

        //public virtual int ConvertIdFromString(int id)
        //{
        //    return (int)TypeDescriptor.GetConverter(typeof(int)).ConvertFrom(id);
        //}

        public virtual async Task<DtoResult> CreateAsync(Article article, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }
            Context.Add(article);
            await SaveChanges(cancellationToken);
            return DtoResult.Success;
        }

        public virtual async Task<DtoResult> DeleteAsync(Article article, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            Context.Remove(article);
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



        public virtual Task<Article> FindByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            //var articleId = ConvertIdFromString(userId);
            return ArticlesSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<DtoResult> UpdateAsync(Article article, CancellationToken cancellationToken = default(CancellationToken))
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
