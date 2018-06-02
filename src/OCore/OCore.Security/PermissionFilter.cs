using System.Threading.Tasks;
using OCore.Security.Permissions;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.AspNetCore.Authorization
{
    /// <summary>
    /// 该过滤器应用用 <see cref="Controller"/> 或 <see cref="Action"/>上.
    /// 只有当请求的用户拥有 <see cref="AllowPermission"/> 属性内指定的权限才能访问该方法.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        public PermissionFilter(Permission allowPermission)
        {
            AllowPermission = allowPermission;
        }

        //Allow
        public Permission AllowPermission { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, AllowPermission);
            if (!authorizationResult)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}
