using Microsoft.AspNetCore.Http;
using XCore.DisplayManagement.Theming;
using System;
using System.Threading.Tasks;

namespace XCore.Admin
{
    public class AdminThemeSelector : IThemeSelector
    {
        //private readonly IAdminThemeService _adminThemeService;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public AdminThemeSelector(
        //    IAdminThemeService adminThemeService,
        //    IHttpContextAccessor httpContextAccessor
        //    )
        //{
        //    _adminThemeService = adminThemeService;
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public Task<ThemeSelectorResult> GetThemeAsync()
        {
            //if (AdminAttribute.IsApplied(_httpContextAccessor.HttpContext))
            //{
            //    string adminThemeName = await _adminThemeService.GetAdminThemeNameAsync();
            //    if (String.IsNullOrEmpty(adminThemeName))
            //    {
            //        return null;
            //    }

            //    return new ThemeSelectorResult
            //    {
            //        Priority = 100,
            //        ThemeName = adminThemeName
            //    };
            //}
            return Task.FromResult(new ThemeSelectorResult
            {
                Priority = 0,
                ThemeName = "TheAdmin"
            });
        }
    }
}
