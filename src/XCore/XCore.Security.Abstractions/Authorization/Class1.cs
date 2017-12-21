using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XCore.Security
{
    public interface IPermissionsAuthorizeData
    {
        string Permissions { get; set; }
 
        string AuthenticationSchemes { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionsAuthorizeAttribute : Attribute, IPermissionsAuthorizeData
    {
        public string Permissions { get; set; }

        public bool RequireAllPermissions { get; set; }

        public string AuthenticationSchemes { get; set; }

        public PermissionsAuthorizeAttribute(params string[] permissions)
        {
            if (permissions!=null)
            {
                Permissions = string.Join(",", permissions);
            }
        }
    }

    public interface IPermissionsAuthorizationHelper
    {
        Task AuthorizeAsync(IEnumerable<PermissionsAuthorizeAttribute> authorizeAttributes);

        Task AuthorizeAsync(MethodInfo methodInfo, Type type);
    }

    //internal class AuthorizationHelper : IPermissionsAuthorizationHelper
    //{
    //    public IAbpSession AbpSession { get; set; }
    //    public IPermissionChecker PermissionChecker { get; set; }
    //    public IFeatureChecker FeatureChecker { get; set; }
    //    public ILocalizationManager LocalizationManager { get; set; }

    //    private readonly IFeatureChecker _featureChecker;
    //    private readonly IAuthorizationConfiguration _authConfiguration;

    //    public AuthorizationHelper(IFeatureChecker featureChecker, IAuthorizationConfiguration authConfiguration)
    //    {
    //        _featureChecker = featureChecker;
    //        _authConfiguration = authConfiguration;
    //        AbpSession = NullAbpSession.Instance;
    //        PermissionChecker = NullPermissionChecker.Instance;
    //        LocalizationManager = NullLocalizationManager.Instance;
    //    }

    //    public async Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes)
    //    {
    //        if (!_authConfiguration.IsEnabled)
    //        {
    //            return;
    //        }

    //        if (!AbpSession.UserId.HasValue)
    //        {
    //            throw new AbpAuthorizationException(
    //                LocalizationManager.GetString(AbpConsts.LocalizationSourceName, "CurrentUserDidNotLoginToTheApplication")
    //                );
    //        }

    //        foreach (var authorizeAttribute in authorizeAttributes)
    //        {
    //            await PermissionChecker.AuthorizeAsync(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
    //        }
    //    }

    //    public async Task AuthorizeAsync(MethodInfo methodInfo, Type type)
    //    {
    //        await CheckFeatures(methodInfo, type);
    //        await CheckPermissions(methodInfo, type);
    //    }

    //    private async Task CheckFeatures(MethodInfo methodInfo, Type type)
    //    {
    //        var featureAttributes = ReflectionHelper.GetAttributesOfMemberAndType<RequiresFeatureAttribute>(methodInfo, type);

    //        if (featureAttributes.Count <= 0)
    //        {
    //            return;
    //        }

    //        foreach (var featureAttribute in featureAttributes)
    //        {
    //            await _featureChecker.CheckEnabledAsync(featureAttribute.RequiresAll, featureAttribute.Features);
    //        }
    //    }

    //    private async Task CheckPermissions(MethodInfo methodInfo, Type type)
    //    {
    //        if (!_authConfiguration.IsEnabled)
    //        {
    //            return;
    //        }

    //        if (AllowAnonymous(methodInfo, type))
    //        {
    //            return;
    //        }

    //        var authorizeAttributes =
    //            ReflectionHelper
    //                .GetAttributesOfMemberAndType(methodInfo, type)
    //                .OfType<IAbpAuthorizeAttribute>()
    //                .ToArray();

    //        if (!authorizeAttributes.Any())
    //        {
    //            return;
    //        }

    //        await AuthorizeAsync(authorizeAttributes);
    //    }

    //    private static bool AllowAnonymous(MemberInfo memberInfo, Type type)
    //    {
    //        return ReflectionHelper
    //            .GetAttributesOfMemberAndType(memberInfo, type)
    //            .OfType<IAbpAllowAnonymousAttribute>()
    //            .Any();
    //    }
    //}
}
