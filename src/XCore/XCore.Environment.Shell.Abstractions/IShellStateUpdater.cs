using System.Threading.Tasks;

namespace XCore.Environment.Shell
{
    public interface IShellStateUpdater
    {
        Task ApplyChanges();
    }
}
