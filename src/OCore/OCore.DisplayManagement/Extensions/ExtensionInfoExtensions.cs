using OCore.DisplayManagement.Extensions;
using OCore.Environment.Extensions;
namespace OCore.DisplayManagement
{
    public static class ExtensionInfoExtensions
    {
        public static bool IsTheme(this IExtensionInfo extensionInfo)
        {
            return extensionInfo is IThemeExtensionInfo;
        }
    }
}
