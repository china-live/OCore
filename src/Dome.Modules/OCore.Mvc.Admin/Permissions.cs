using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OCore.Security;
using OCore.Security.Permissions;

namespace OCore.Mvc.Admin
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageRoles = new Permission("ManageRoles", "角色管理");
        public static readonly Permission ManageUsers = new Permission("ManageUsers", "用户管理");
        public static readonly Permission ManageArticles = new Permission("ManageArticles", "新闻管理");

        public static readonly Permission ManageSites = new Permission("ManageSites", "网站管理", new[] { ManageArticles });

        //public static readonly Permission ManageSites = new Permission("ManageSites", "网站管理", new[] { ManageArticles });

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                ManageRoles,
                ManageUsers,
                ManageSites,
                ManageArticles
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = GetPermissions()
                },
            };
        }

    }

   
}
