using System.Collections.Generic;

namespace XCore.Environment.Extensions.Features
{
    public interface IFeaturesProvider
    {
        IEnumerable<IFeatureInfo> GetFeatures(IExtensionInfo extensionInfo,IManifestInfo manifestInfo);
    }
}
