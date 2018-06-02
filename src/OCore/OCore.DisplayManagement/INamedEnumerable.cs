using System.Collections.Generic;

namespace OCore.DisplayManagement
{
    public interface INamedEnumerable<T> : IEnumerable<T>
    {
        IList<T> Positional { get; }
        IDictionary<string, T> Named { get; }
    }
}
