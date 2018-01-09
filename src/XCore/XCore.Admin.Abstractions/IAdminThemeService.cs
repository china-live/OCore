using System.Threading.Tasks;
using XCore.Environment.Extensions;

namespace XCore.Admin
{
    public interface IAdminThemeService
    {
        Task<IExtensionInfo> GetAdminThemeAsync();
        Task SetAdminThemeAsync(string themeName);
        Task<string> GetAdminThemeNameAsync();
    }
}
