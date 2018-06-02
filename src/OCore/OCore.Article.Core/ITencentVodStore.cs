using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OCore.Common;

namespace OCore.Article
{
    public interface ITencentVodStore<TTencentVod> : IDisposable where TTencentVod : class
    {
        Task<DtoResult> CreateAsync(TTencentVod vod, CancellationToken cancellationToken);
        Task<DtoResult> UpdateAsync(TTencentVod vod, CancellationToken cancellationToken);

        Task<DtoResult> DeleteAsync(TTencentVod vod, CancellationToken cancellationToken);

        Task<TTencentVod> FindByIdAsync(int id, CancellationToken cancellationToken);
    }

    public interface IQueryableTencentVodStore<TTencentVod> : ITencentVodStore<TTencentVod> where TTencentVod : class
    {
        IQueryable<TTencentVod> TencentVods { get; }
    }
}
