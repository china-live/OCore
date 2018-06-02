using System.Threading.Tasks;

namespace OCore.Environment.Shell
{
    public interface IShellStateUpdater
    {
        Task ApplyChanges();
    }
}
