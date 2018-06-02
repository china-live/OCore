using System.Collections.Generic;
using System.IO;
using System.Linq;
using OCore.Environment.Extensions.Features;

namespace OCore.Environment.Extensions
{
    /// <summary>
    /// 内部扩展信息
    /// </summary>
    public class InternalExtensionInfo : IExtensionInfo
    {
        private readonly string _id;
        private readonly string _subPath;
        private readonly IManifestInfo _manifestInfo;
        private readonly IEnumerable<IFeatureInfo> _features;

        public InternalExtensionInfo(string subPath)
        {
            _id = Path.GetFileName(subPath);
            _subPath = subPath;

            _manifestInfo = new NotFoundManifestInfo(subPath);
            _features = Enumerable.Empty<IFeatureInfo>();
        }

        public string Id => _id;
        public string SubPath => _subPath;
        public IManifestInfo Manifest => _manifestInfo;
        public IEnumerable<IFeatureInfo> Features => _features;
        public bool Exists => _manifestInfo.Exists;
    }
}
