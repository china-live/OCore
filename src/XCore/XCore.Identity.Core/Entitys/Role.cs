using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.Entities;

namespace XCore.Identity
{
    public class Role : IdentityRole, IEntity
    {
        public JObject Properties { get; set; } = new JObject();
    }
    public class RoleClaim : IdentityRoleClaim<string>, IEntity
    {
        public JObject Properties { get; set; } = new JObject();
    }

    public class UserRole : IdentityUserRole<string>, IEntity
    {
        public JObject Properties { get; set; } = new JObject();
    }
}
