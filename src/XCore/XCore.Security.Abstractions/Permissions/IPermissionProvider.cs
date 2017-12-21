using System.Collections.Generic;
using System.Text;

namespace XCore.Security.Permissions
{
    /// <summary>
    /// Implemented by modules to enumerate the types of permissions
    /// the which may be granted
    /// 在模块中实现该接口，在实现类中定义该模块可授予的权限类型
    /// </summary>
    public interface IPermissionProvider
    {
        IEnumerable<Permission> GetPermissions();
        IEnumerable<PermissionStereotype> GetDefaultStereotypes();
    }

    /*
     * 问题，为什么IPermissionProvider中不直接报含Permission列表，而要在中间加一层PermissionStereotype
     * 
     * 初步探明PermissionStereotype拥有下列名称，从名称中可以看到它也是对权限进行分类（或分组），那为什么不直接用Category字段区分呢？
     * 这可能是因为单纯一个Category不足以描述各种状态。
     * 比如要把一群人进行分类，该怎么分？
     * 首先，确定按什么来分，比如：按国籍、按肤色、按年龄等等，然后才是真正进行分类。
     * administrator
     * editor
     * moderator
     * author
     * contributor
     * authenticated
     * anonymous
     */


    /// <summary>
    /// Stereotype 一词原意是指旧时印刷术中的铅板，用在这里可以形象、生动的表明这一组权限是一个模板刻出来的。
    /// </summary>
    public class PermissionStereotype //突然发现英语单词挺有意思的，这种语境怎么表述（翻译）都没有那种味道，跟汉语中的成语是一样一样的。
    {
        public string Name { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
