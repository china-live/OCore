using System.Security.Claims;
using System.Threading.Tasks;

namespace OCore.Security.Services
{
    /// <summary>
    /// Membership 用于验证用户凭据
    /// </summary>
    public interface IMembershipService
    {
        Task<IUser> GetUserAsync(string userName);
        Task<bool> CheckPasswordAsync(string userName, string password);
        Task<ClaimsPrincipal> CreateClaimsPrincipal(IUser user);
    }
}
