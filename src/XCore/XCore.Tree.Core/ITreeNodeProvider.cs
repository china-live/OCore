using System.Collections.Generic;

namespace XCore.Tree
{
    public interface ITreeNodeProvider
    {
        IEnumerable<Node> GetNodes();
    }
}
