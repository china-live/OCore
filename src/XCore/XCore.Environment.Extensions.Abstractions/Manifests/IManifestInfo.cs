using System;
using System.Collections.Generic;
using System.Linq;
using XCore.Modules.Manifest;

namespace XCore.Environment.Extensions
{
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
        ModuleAttribute ModuleInfo { get; }
    }

    public class NotFoundManifestInfo : IManifestInfo
    {
        public NotFoundManifestInfo(string subPath)
        {
        }

        public bool Exists => false;
        public string Name => null;
        public string Description => null;
        public string Type => null;
        public string Author => null;
        public string Website => null;
        public Version Version => null;
        public IEnumerable<string> Tags => Enumerable.Empty<string>();
        public ModuleAttribute ModuleInfo => null;
    }
}
