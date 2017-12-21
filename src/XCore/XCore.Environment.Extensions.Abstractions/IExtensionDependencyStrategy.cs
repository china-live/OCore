using XCore.Environment.Extensions.Features;

namespace XCore.Environment.Extensions
{
    public interface IExtensionDependencyStrategy
    {
        bool HasDependency(IFeatureInfo observer, IFeatureInfo subject);
    }
}