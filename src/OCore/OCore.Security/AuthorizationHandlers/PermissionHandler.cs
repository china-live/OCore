using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OCore.Security.Permissions;

namespace OCore.Security.AuthorizationHandlers
{
    /// <summary>
    /// This authorization handler ensures that the user has the required permission.
    /// 该授权处理类用来确保用户具有所需的权限。
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!(bool)context?.User?.Identity?.IsAuthenticated) //用户是否未通过验证。
            {
                return Task.CompletedTask;
            }
            else if (context.User.HasClaim(Permission.ClaimType, requirement.Permission.Name))//已通过验证，检查是否具有某个权限
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
