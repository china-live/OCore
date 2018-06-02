using Newtonsoft.Json.Linq;
using System;
using OCore.Entities;

namespace OCore.Article
{
    public class Photo : IEntity {
        public JObject Properties { get; set; } = new JObject();

        public virtual int Id { get; set; }
    }

    public class Album : IEntity {
        public JObject Properties { get; set; } = new JObject();

        public virtual int Id { get; set; }

        public virtual string CategoryName { get; set; }

        public virtual string Path { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsCover { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort
        {
            get; set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime
        {
            get; set;
        }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime UpdateTime
        {
            get;
            set;
        }

    }

    public class Article : IEntity
    {
        public JObject Properties { get; set; } = new JObject();

        public virtual int Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public virtual string CategoryName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 内容摘要
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        public virtual string Content
        {
            get;set;
        }

        /// <summary>
        /// 文章来源
        /// </summary>
        public virtual string Source { get; set; }
        /// <summary>
        /// 文章封面图片
        /// </summary>
        public virtual string CoverImg { get; set; }
        /// <summary>
        /// SEO标题
        /// </summary>
        public virtual string SeoTitle { set; get; }
        /// <summary>
        /// SEO关健字
        /// </summary>
        public virtual string SeoKeywords { set; get; }
        /// <summary>
        /// SEO描述
        /// </summary>
        public virtual string SeoDescription { set; get; }

        /// <summary>
        /// TAG标签逗号分隔
        /// </summary>
        public virtual string Tags {set;get;}

        /// <summary>
        /// 浏览次数
        /// </summary>
        public virtual int Click
        {
            get;set;
        }

        /// <summary>
        /// 喜欢数量
        /// </summary>
        public virtual int LikeCount
        {
            get;set;
        }
        /// <summary>
        /// 状态0未发布 1发布 -1锁定
        /// </summary>
        public virtual int Status
        {
            get;set;
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public virtual bool IsTop
        {
            get;set;
        }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public virtual bool IsRed
        {
            get;set;
        }
        /// <summary>
        /// 是否热门
        /// </summary>
        public virtual bool IsHot
        {
            get;set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort
        {
            get;set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime
        {
            get; set;
        }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime UpdateTime
        {
            get;
            set;
        }
    }

    //public class ArticleOptions
    //{
    //    public string CategoryName { get; set; }
    //}
}
