using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCore.Common;
using System.Linq;

namespace XCore.Article
{
    public interface IArticleStore<TArticle> : IDisposable where TArticle : class
    {
        Task<DtoResult> CreateAsync(TArticle article, CancellationToken cancellationToken);
        Task<DtoResult> UpdateAsync(TArticle article, CancellationToken cancellationToken);

        Task<DtoResult> DeleteAsync(TArticle article, CancellationToken cancellationToken);

        Task<TArticle> FindByIdAsync(int id, CancellationToken cancellationToken);
    }

    public interface IQueryableArticleStore<TArticle> : IArticleStore<TArticle> where TArticle : class
    {
        IQueryable<TArticle> Articles { get; }
    }

    
}
