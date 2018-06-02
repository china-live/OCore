using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCore.Security.Services
{
    public interface IRoleProvider
    {
        Task<IEnumerable<string>> GetRoleNamesAsync();
    }
}
