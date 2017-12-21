using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using XCore.Environment.Extensions.Features;
using XCore.Environment.Extensions.Manifests;

namespace XCore.Environment.Extensions
{
    /// <summary>
    /// 提供扩展模块信息
    /// </summary>
    public interface IExtensionInfo
    {
        string Id { get; }

        /// <summary>
        /// 获取该扩展(模块)的文件信息
        /// </summary>
        IFileInfo ExtensionFileInfo { get; }

        /// <summary>
        /// 获取该扩展(模块)的文件路径
        /// </summary>
        string SubPath { get; }

        bool Exists { get; }

        /// <summary>
        /// 获取该扩展(模块)的描述文件
        /// </summary>
        IManifestInfo Manifest { get; }

        /// <summary>
        /// 获取该扩展(模块)可提供的功能列表
        /// </summary>
        IEnumerable<IFeatureInfo> Features { get; }
    }
}
