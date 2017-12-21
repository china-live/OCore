using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCore.Security.Services
{
    public interface IRoleProvider
    {
        Task<IEnumerable<string>> GetRoleNamesAsync();
    }
}
