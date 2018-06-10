using Microsoft.AspNetCore.Identity;
using System;
using OCore.Entities;
using Newtonsoft.Json.Linq;

namespace OCore.Identity
{
    public class User : IdentityUser, IEntity
    {
        //public JObject Properties { get; set; } = new JObject();

        //public virtual string Surname { get; set; }

        public virtual string FullName { get; set; }

        public virtual bool IsActive { get; set; }
    }
    public class UserClaim:IdentityUserClaim<string>, IEntity
    {
        //public JObject Properties { get; set; } = new JObject();
    }
    
    public class UserLogin : IdentityUserLogin<string>, IEntity
    {
        //public JObject Properties { get; set; } = new JObject();
    }

    public class UserToken : IdentityUserToken<string>, IEntity
    {
        //public JObject Properties { get; set; } = new JObject();
    }
}
