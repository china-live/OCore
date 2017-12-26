using XCore.Environment.Extensions.Features;
using XCore.Environment.Extensions.Manifests;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore.Environment.Extensions
{
    /// <summary>
    /// 内部扩展信息
    /// </summary>
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

        /// <summary>
        /// 指示扩展程序集（dll文件）和 清单文件（Manifest）是否都存在
        /// </summary>
        public bool Exists => _fileInfo.Exists && _manifestInfo.Exists;
    }
}
