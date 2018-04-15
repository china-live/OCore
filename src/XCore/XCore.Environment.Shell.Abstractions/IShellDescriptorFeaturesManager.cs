using XCore.Environment.Shell.Descriptor.Models;
using System.Threading.Tasks;
using XCore.Environment.Extensions.Features;
using System.Collections.Generic;

namespace XCore.Environment.Shell
{
    public delegate void FeatureDependencyNotificationHandler(string messageFormat, IFeatureInfo feature, IEnumerable<IFeatureInfo> features);
    public interface IShellDescriptorFeaturesManager
    {
        Task<IEnumerable<IFeatureInfo>> EnableFeaturesAsync(
            ShellDescriptor shellDescriptor, IEnumerable<IFeatureInfo> features);
        Task<IEnumerable<IFeatureInfo>> EnableFeaturesAsync(
            ShellDescriptor shellDescriptor, IEnumerable<IFeatureInfo> features, bool force);
        Task<IEnumerable<IFeatureInfo>> DisableFeaturesAsync(
            ShellDescriptor shellDescriptor, IEnumerable<IFeatureInfo> features);
        Task<IEnumerable<IFeatureInfo>> DisableFeaturesAsync(
            ShellDescriptor shellDescriptor, IEnumerable<IFeatureInfo> features, bool force);
    }
}