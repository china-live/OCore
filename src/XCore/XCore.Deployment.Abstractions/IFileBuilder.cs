using System.Threading.Tasks;

namespace XCore.Deployment
{
    public interface IFileBuilder
    {
        Task SetFileAsync(string subpath, byte[] content);
    }
}
