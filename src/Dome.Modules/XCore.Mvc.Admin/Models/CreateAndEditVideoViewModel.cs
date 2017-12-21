using System;
using System.ComponentModel.DataAnnotations;

namespace XCore.Mvc.Admin.Models
{
    public class CreateAndEditVideoViewModel
    {
        public int Id { get; set; }

        [StringLength(255)]
        public  string FileId { get; set; }

        [StringLength(255)]
        public  string AppId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public  string CategoryName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(255)]
        public  string Title { get; set; }

        /// <summary>
        /// 内容摘要
        /// </summary>
        public  string Description { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public  string CoverImg { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public  int Click
        {
            get; set;
        }

        /// <summary>
        /// 喜欢数量
        /// </summary>
        public  int LikeCount
        {
            get; set;
        }
        /// <summary>
        /// 状态0未发布 1发布 -1锁定
        /// </summary>
        public int? Status { get; set; }
 

        /// <summary>
        /// 是否置顶
        /// </summary>
        public  bool IsTop
        {
            get; set;
        }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public  bool IsRed
        {
            get; set;
        }
        /// <summary>
        /// 是否热门
        /// </summary>
        public  bool IsHot
        {
            get; set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public  int Sort
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
    }
}
