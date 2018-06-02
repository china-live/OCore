namespace OCore.ResourceManagement
{
    public interface IResourceManifestProvider
    {
        void BuildManifests(IResourceManifestBuilder builder);
    }
}
