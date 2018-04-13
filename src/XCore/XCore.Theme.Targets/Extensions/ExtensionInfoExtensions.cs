using XCore.Environment.Extensions;
namespace XCore.Theme
{
    public static class ExtensionInfoExtensions
    {
        public static bool IsTheme(this IExtensionInfo extensionInfo)
        {
            return extensionInfo is IThemeExtensionInfo;
        }
    }
}
