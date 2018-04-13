﻿using System.Collections.Generic;
using XCore.Security.Permissions;

namespace XCore.Admin
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission AccessAdminPanel = new Permission("AccessAdminPanel", "Access admin panel");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = GetPermissions()
                }
            };
        }
    }
}
