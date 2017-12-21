using XCore.Environment.Extensions.Features;

namespace XCore.Environment.Extensions
{
    public interface IExtensionPriorityStrategy
    {
        int GetPriority(IFeatureInfo feature);
    }
}