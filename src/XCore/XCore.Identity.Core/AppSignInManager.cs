using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace XCore.Identity
{
    public class AppSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        public AppSignInManager(
            UserActiveManager<TUser> userActiveManager,
            UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            IOptions<XCoreUserOptions> optionsAccessor2,
            ILogger<SignInManager<TUser>> logger,
            IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            UserActiveManager = userActiveManager;
            Options2 = optionsAccessor2?.Value ?? new XCoreUserOptions();
        }

        public XCoreUserOptions Options2 { get; set; }

        public UserActiveManager<TUser> UserActiveManager { get; set; }

        public override async Task<bool> CanSignInAsync(TUser user)
        {
            //base
            //if (Options.SignIn.RequireConfirmedEmail && !(await UserManager.IsEmailConfirmedAsync(user)))
            //{
            //    Logger.LogWarning(0, "User {userId} cannot sign in without a confirmed email.", await UserManager.GetUserIdAsync(user));
            //    return false;
            //}
            //if (Options.SignIn.RequireConfirmedPhoneNumber && !(await UserManager.IsPhoneNumberConfirmedAsync(user)))
            //{
            //    Logger.LogWarning(1, "User {userId} cannot sign in without a confirmed phone number.", await UserManager.GetUserIdAsync(user));
            //    return false;
            //}
            if (await base.CanSignInAsync(user) && Options2.RequireActive && !await UserActiveManager.IsActiveAsync(user))
            {
                Logger.LogWarning(2, "User {userId} cannot sign in without a not active.", await UserManager.GetUserIdAsync(user));
                return false;
            }

            return true;
        }
    }
}
