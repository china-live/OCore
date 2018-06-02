using System.Threading.Tasks;

namespace OCore.Modules
{
    public interface IModularTenantEvents
    {
        Task ActivatingAsync();
        Task ActivatedAsync();
        Task TerminatingAsync();
        Task TerminatedAsync();
    }
}
