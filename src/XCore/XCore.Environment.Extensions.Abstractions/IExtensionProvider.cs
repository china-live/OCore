using XCore.Environment.Extensions.Manifests;

namespace XCore.Environment.Extensions
{
    /// <summary>
    /// 从该接口的实现类获取扩展（模块）
    /// </summary>
    public interface IExtensionProvider
    {
        int Order { get; }
        IExtensionInfo GetExtensionInfo(IManifestInfo manifestInfo, string subPath);
    }
}