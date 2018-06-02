using OCore.Environment.Extensions.Features;

namespace OCore.Environment.Extensions
{
    public class ExtensionPriorityStrategy : IExtensionPriorityStrategy
    {
        public int GetPriority(IFeatureInfo feature)
        {
            return feature.Priority;
        }
    }
}