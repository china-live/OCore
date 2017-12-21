using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Security
{
    public class Role
    {
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }
        public List<RoleClaim> RoleClaims { get; } = new List<RoleClaim>();
    }
}
