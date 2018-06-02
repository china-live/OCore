using System.ComponentModel.DataAnnotations;

namespace OCore.Mvc.Admin.Models
{

    public class CreateAndEditArticleViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// 内容摘要
        /// </summary>
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        [StringLength(80000)]
        public string Content
        {
            get; set;
        }

        /// <summary>
        /// 封面图片
        /// </summary>
        [StringLength(255)]
        public virtual string CoverImg { get; set; }

        /// <summary>
        /// 文章来源
        /// </summary>
        [StringLength(50)]
        public string Source { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        [StringLength(500)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public int? Status { get; set; }
    }
}
