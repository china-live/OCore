using XCore.DisplayManagement.Extensions;
using XCore.Environment.Extensions;
namespace XCore.DisplayManagement
{
    public static class ExtensionInfoExtensions
    {
        public static bool IsTheme(this IExtensionInfo extensionInfo)
        {
            return extensionInfo is IThemeExtensionInfo;
        }
    }
}
