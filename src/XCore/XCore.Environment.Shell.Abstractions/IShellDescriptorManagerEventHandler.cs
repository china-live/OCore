using XCore.Environment.Shell.Descriptor.Models;
using System.Threading.Tasks;

namespace XCore.Environment.Shell
{
    /// <summary>
    /// Represent and event handler for shell descriptor.
    /// 定义用于 shell descriptor 改变时的事件处理接口
    /// </summary>
    public interface IShellDescriptorManagerEventHandler 
    {
        /// <summary>
        /// Triggered whenever a shell descriptor has changed.
        /// </summary>
        Task Changed(ShellDescriptor descriptor, string tenant);
    }
}