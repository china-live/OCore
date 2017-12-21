using System.Collections.Generic;

namespace XCore.ResourceManagement
{
    public interface IResourceManifestState
    {
        IEnumerable<ResourceManifest> ResourceManifests { get; set; }
    }

    public class ResourceManifestState : IResourceManifestState
    {
        public IEnumerable<ResourceManifest> ResourceManifests { get; set; }

    }

}
