using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace OCore.Environment.Shell
{
    public interface IShellSettingsConfigurationProvider
    {
        void AddSource(IConfigurationBuilder builder);
        void SaveToSource(string name, IDictionary<string, string> configuration);

        int Order { get; }
    }
}
