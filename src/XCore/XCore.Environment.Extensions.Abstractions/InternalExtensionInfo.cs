using XCore.Environment.Extensions.Features;
using XCore.Environment.Extensions.Manifests;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore.Environment.Extensions
{
    public class InternalExtensionInfo : IExtensionInfo
    {
        private readonly IFileInfo _fileInfo;
        private readonly string _subPath;
        private readonly IManifestInfo _manifestInfo;
        private readonly IEnumerable<IFeatureInfo> _features;

        public InternalExtensionInfo(string subPath)
        {
            _subPath = subPath;

            _fileInfo = new NotFoundFileInfo(subPath);
            _manifestInfo = new NotFoundManifestInfo(subPath);
            _features = Enumerable.Empty<IFeatureInfo>();
        }

        public string Id => _fileInfo.Name;
        public IFileInfo ExtensionFileInfo => _fileInfo;
        public string SubPath => _subPath;
        public IManifestInfo Manifest => _manifestInfo;
        public IEnumerable<IFeatureInfo> Features => _features;
        public bool Exists => _fileInfo.Exists && _manifestInfo.Exists;
    }
}
