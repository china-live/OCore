using System.Collections.Generic;
using System.Threading.Tasks;
using OCore.DisplayManagement.Extensions;
using OCore.Environment.Extensions;

namespace OCore.DisplayManagement.Theming
{
    public interface IThemeManager
    {
        Task<IExtensionInfo> GetThemeAsync();
    }
    public class ThemeManager : IThemeManager
    {
        private readonly IEnumerable<IThemeSelector> _themeSelectors;
        private readonly IExtensionManager _extensionManager;

        private IExtensionInfo _theme;

        public ThemeManager(
            IEnumerable<IThemeSelector> themeSelectors,
            IExtensionManager extensionManager)
        {
            _themeSelectors = themeSelectors;
            _extensionManager = extensionManager;
        }

        public async Task<IExtensionInfo> GetThemeAsync()
        {
            // 由于性能原因，每个域（请求）只处理当前主题一次。
            //这不能缓存，因为每个请求获得不同的值。

            // For performance reason, processes the current theme only once per scope (request).
            // This can't be cached as each request gets a different value.
            if (_theme == null)
            {
                var themeResults = new List<ThemeSelectorResult>();
                foreach (var themeSelector in _themeSelectors)
                {
                    var themeResult = await themeSelector.GetThemeAsync();
                    if (themeResult != null)
                    {
                        themeResults.Add(themeResult);
                    }
                }

                themeResults.Sort((x, y) => y.Priority.CompareTo(x.Priority));

                if (themeResults.Count == 0)
                {
                    return null;
                }

                // Try to load the theme to ensure it's present尝试加载主题，以确保它的存在。
                foreach (var theme in themeResults)
                {
                    var t = _extensionManager.GetExtension(theme.ThemeName);

                    if (t.Exists)
                    {
                        return _theme = new ThemeExtensionInfo(t);
                    }
                }

                // No valid theme. Don't save the result right now.
                return null;
            }

            return _theme;
        }
    }
}
