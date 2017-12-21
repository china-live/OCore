using System.Security.Claims;
using System.Threading.Tasks;
using XCore.Security.Permissions;
using XCore.Security;
using System;

namespace Microsoft.AspNetCore.Authorization
{
    public static class AuthorizationServiceExtensions
    {
        public static Task<bool> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, Permission permission)
        {
            return AuthorizeAsync(service, user, permission, null);
        }

        public static async Task<bool> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, Permission permission, object resource)
        {
            return (await service.AuthorizeAsync(user, resource, new PermissionRequirement(permission))).Succeeded;
        }
    }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    //public class AppAuthorizeAttribute : AuthorizeAttribute
    //{
 
    //    public string[] Permissions { get; set; }
 
    //    public bool RequireAllPermissions { get; set; }
 
    //    public AppAuthorizeAttribute(params string[] permissions)
    //    {
    //        Permissions = permissions;
    //    }

    //    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    //    {
    //        var httpContext = filterContext.HttpContext;

    //        //if (!httpContext.Request.IsAjaxRequest())
    //        //{
    //        //    base.HandleUnauthorizedRequest(filterContext);
    //        //    return;
    //        //}

    //        //httpContext.Response.StatusCode = httpContext.User.Identity.IsAuthenticated == false
    //        //                          ? (int)System.Net.HttpStatusCode.Unauthorized
    //        //                          : (int)System.Net.HttpStatusCode.Forbidden;

    //        //httpContext.Response.SuppressFormsAuthenticationRedirect = true;
    //        httpContext.Response.End();
    //    }
    //}

}
