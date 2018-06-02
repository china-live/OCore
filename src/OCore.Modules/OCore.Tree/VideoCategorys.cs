using System.Collections.Generic;

namespace OCore.Tree
{
    public class VideoCategorys : ITreeNodeProvider
    {
        public static readonly Node cwRoot = new Node("春晚", "同根同梦春晚网站");
        public static readonly Node cwHrdbn = new Node("华人大拜年", "", cwRoot);
        public static readonly Node cwZtcw = new Node("直通春晚", "", cwRoot);
        public static readonly Node cwJcsp = new Node("精彩视频", "", cwRoot);
        public static readonly Node cwWqhg = new Node("往期回顾", "", cwRoot);

        public IEnumerable<Node> GetNodes()
        {
            return new[]
            {
                cwRoot,
                cwHrdbn,
                cwZtcw,
                cwJcsp,
                cwWqhg
            };
        }
    }
}
