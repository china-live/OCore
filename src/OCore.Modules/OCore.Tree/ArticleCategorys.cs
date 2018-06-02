using System.Collections.Generic;

namespace OCore.Tree
{
    public class ArticleCategorys : ITreeNodeProvider
    {
        public static readonly Node cwRoot = new Node("春晚", "同根同梦春晚网站");
        public static readonly Node cwNews = new Node("新闻动态", "", cwRoot);
        public static readonly Node cwHrkm = new Node("华人楷模", "", cwRoot);
        public static readonly Node cwYxlrw = new Node("影响力人物", "", cwRoot);

        public IEnumerable<Node> GetNodes()
        {
            return new[]
            {
                cwRoot,
                cwNews,
                cwHrkm,
                cwYxlrw
            };
        }
    }
}
