using System.Threading.Tasks;

namespace OCore.Deployment
{
    public interface IFileBuilder
    {
        Task SetFileAsync(string subpath, byte[] content);
    }
}
