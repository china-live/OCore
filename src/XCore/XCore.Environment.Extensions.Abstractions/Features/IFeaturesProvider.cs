using System;
using System.Collections.Generic;
using XCore.Environment.Extensions.Manifests;

namespace XCore.Environment.Extensions.Features
{
    public interface IFeaturesProvider
    {
        IEnumerable<IFeatureInfo> GetFeatures(IExtensionInfo extensionInfo,IManifestInfo manifestInfo);
    }
}
