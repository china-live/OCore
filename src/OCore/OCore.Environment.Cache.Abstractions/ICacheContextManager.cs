using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCore.Environment.Cache
{
    /// <summary>
    /// Provides the discriminator for a specific cache context by requesting all <see cref="ICacheContextProvider"/>
    /// implementations.
    /// </summary>
    public interface ICacheContextManager
    {
        Task<IEnumerable<CacheContextEntry>> GetDiscriminatorsAsync(IEnumerable<string> contexts);
    }
}
