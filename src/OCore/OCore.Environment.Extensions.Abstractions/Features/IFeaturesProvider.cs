using System.Collections.Generic;

namespace OCore.Environment.Extensions.Features
{
    public interface IFeaturesProvider
    {
        IEnumerable<IFeatureInfo> GetFeatures(IExtensionInfo extensionInfo,IManifestInfo manifestInfo);
    }
}
