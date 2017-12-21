using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace XCore.Security.Permissions
{
    /*permission一词本意是做某事的许可，这里简单的翻译为“权限”，表示在系统中有权做什么操作*/
    /// <summary>
    /// 定义访问、操作、修改系统资源时所需的权限
    /// </summary>
    public class Permission
    {
        public const string ClaimType = "Permission";

        public Permission(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public Permission(string name, string description) : this(name)
        {
            Description = description;
        }

        public Permission(string name, string description, IEnumerable<Permission> impliedBy) : this(name, description)
        {
            ImpliedBy = impliedBy;
        }

        /// <summary>
        /// 获取或设置权限的名称，该值尽量简短明了，最重要的一点，不能重复。
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置该权限的补充说明信息，属于辅助字段，让使用都明白该权限项是做什么的，有什么用
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 当权限项太多时，对权限进行分类是很有必要的
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 权限与权限之间也不是完全平等的，它们是有上下级关系的，该字段表示该权限包含的（直属）下级权限列表。
        /// </summary>
        public IEnumerable<Permission> ImpliedBy { get; set; }

        /// <summary>
        /// 该对应的声明
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator Claim(Permission p)
        {
            return new Claim(ClaimType, p.Name);
        }
    }
}
