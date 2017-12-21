using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace XCore.Environment.Extensions.Manifests
{
    /// <summary>
    /// Madifest是个XML的描述文件，对于每个DLL有DLL的Manifest文件，对于每个应用程序Application也有自己的Manifest。
    /// XCore和Orchard一样，描述文件通常是Module.txt文件，存储格式为Yaml
    /// </summary>
    public interface IManifestInfo
    {
        bool Exists { get; }
        string Name { get; }
        string Description { get; }
        string Type { get; }
        string Author { get; }
        string Website { get; }
        Version Version { get; }
        IEnumerable<string> Tags { get; }
        IConfigurationRoot ConfigurationRoot { get; }
    }
}
