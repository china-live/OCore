using Microsoft.Extensions.Primitives;

namespace OCore.Environment.Cache
{
    public interface ISignal
    {
        IChangeToken GetToken(string key);

        void SignalToken(string key);
    }
}
