using System.Threading.Tasks;
using XCore.DisplayManagement.Theming;

namespace XCore.Themes.Services
{
    /// <summary>
    /// 安全模式，当设置的主题未找到主题时退回到默认主题。
    /// </summary>
    public class SafeModeThemeSelector : IThemeSelector
    {
        public Task<ThemeSelectorResult> GetThemeAsync()
        {
            return Task.FromResult(new ThemeSelectorResult
            {
                Priority = -100,
                ThemeName = "SafeMode"
            });
        }
    };

    public class SiteThemeSelector : IThemeSelector
    {
        public Task<ThemeSelectorResult> GetThemeAsync()
        {
            return Task.FromResult(new ThemeSelectorResult
            {
                Priority = 0,
                ThemeName = "TheTheme"
            });
        }
    }
}
