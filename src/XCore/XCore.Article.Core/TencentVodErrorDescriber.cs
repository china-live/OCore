using XCore.Common;
using XCore.Article.Core.Properties;

namespace XCore.Article
{
    public class TencentVodErrorDescriber
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
