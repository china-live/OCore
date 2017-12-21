using XCore.Environment.Extensions.Features;
using XCore.Environment.Extensions.Manifests;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Linq;

namespace XCore.Environment.Extensions
{
    public class NotFoundExtensionInfo : IExtensionInfo
    {
        private readonly IEnumerable<IFeatureInfo> _featureInfos;
        private readonly string _extensionId;
        private readonly IFileInfo _fileInfo;
        private readonly IManifestInfo _manifestInfo;

        public NotFoundExtensionInfo(string extensionId)
        {
            _featureInfos = Enumerable.Empty<IFeatureInfo>();
            _extensionId = extensionId;
            _fileInfo = new NotFoundFileInfo(extensionId);
            _manifestInfo = new NotFoundManifestInfo(extensionId);
        }

        public bool Exists => false;

        public IFileInfo ExtensionFileInfo => _fileInfo;

        public IEnumerable<IFeatureInfo> Features => _featureInfos;

        public string Id => _extensionId;

        public IManifestInfo Manifest => _manifestInfo;

        public string SubPath => _extensionId;
    }
}
