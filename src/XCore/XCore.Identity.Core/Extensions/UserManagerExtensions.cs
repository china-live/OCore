using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCore.Identity.Extensions
{
    /// <summary>
    ///     Extension methods for UserManager
    /// </summary>
    public static class UserManagerExtensions
    {
        //根据业务定制扩展
        //1.一个手机号只能注册绑定到一个用户
        //2.用户姓名（FullName）不能重复

        public static async Task<TUser> FindByPhoneNumberAsync<TUser>(this UserManager<TUser> manager, string phoneNumber) where TUser :User
        {
            return await Task.Run(() => manager.Users.SingleOrDefault(c => c.PhoneNumber == phoneNumber));
        }
        
        public static async Task<TUser> FindByFullNameAsync<TUser>(this UserManager<TUser> manager, string fullName) where TUser : User
        {
            return await Task.Run(()=> manager.Users.SingleOrDefault(c => c.FullName == fullName));
        }

        public static Task<string> GetFullNameAsync<TUser>(this UserManager<TUser> manager, TUser user) where TUser : User
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.FullName);
        }
    }

    

    public static class RoleManagerExtensions {
        //public static async Task<IdentityResult> AddClaimsAsync(this RoleManager<Role> manager, Role role, IEnumerable<Claim> claims)
        //{
        //    //ThrowIfDisposed();
        //    var claimStore = Store as IRoleClaimStore<Role>;
        //    if (claims == null)
        //    {
        //        throw new ArgumentNullException(nameof(claims));
        //    }
        //    if (role == null)
        //    {
        //        throw new ArgumentNullException(nameof(role));
        //    }

        //    await claimStore.AddClaimsAsync(role, claims, CancellationToken.None);
        //    return await manager.UpdateRoleAsync(role);
        //}
    }
}
