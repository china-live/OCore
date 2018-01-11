using XCore.Environment.Extensions;
using XCore.Environment.Extensions.Features;

namespace XCore.DisplayManagement.Extensions
{
    public class ThemeExtensionDependencyStrategy : IExtensionDependencyStrategy
    {
        public bool HasDependency(IFeatureInfo observer, IFeatureInfo subject)
        {
            if (observer.Extension.Manifest.IsTheme())
            {
                if (!subject.Extension.Manifest.IsTheme())
                    return true;
            }

            return false;
        }
    }
}
