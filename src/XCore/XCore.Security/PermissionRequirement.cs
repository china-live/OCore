using System;
using Microsoft.AspNetCore.Authorization;
using XCore.Security.Permissions;

namespace XCore.Security
{
    /// <summary>
    /// Requirement 要求; 必要条件;这里表示“用户”访问系统资源时应具有的某个权限
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement/*表示授权需求*/
    {
        public PermissionRequirement(Permission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            Permission = permission;
        }

        public Permission Permission { get; set; }
    }
}


