using OCore.Common;
using OCore.Article.Core.Properties;

namespace OCore.Article
{
    public class ArticleErrorDescriber
    {
        public virtual DtoError DefaultError()
        {
            return new DtoError
            {
                Code = nameof(DefaultError),
                Description = Resources.DefaultError
            };
        }

        public virtual DtoError ConcurrencyFailure()
        {
            return new DtoError
            {
                Code = nameof(ConcurrencyFailure),
                Description = Resources.ConcurrencyFailure
            };
        }
    }
}
