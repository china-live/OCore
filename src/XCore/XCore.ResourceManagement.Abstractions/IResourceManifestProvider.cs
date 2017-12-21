namespace XCore.ResourceManagement
{
    public interface IResourceManifestProvider
    {
        void BuildManifests(IResourceManifestBuilder builder);
    }
}
