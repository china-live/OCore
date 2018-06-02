using System.Collections.Generic;

namespace OCore.Tree
{
    public interface ITreeNodeProvider
    {
        IEnumerable<Node> GetNodes();
    }
}
