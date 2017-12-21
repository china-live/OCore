using XCore.Security.Permissions;
using System;

namespace XCore.Security
{
    public class StandardPermissions
    {
        public static readonly Permission SiteOwner = new Permission("SiteOwner", "Site Owners Permission");
    }
}
