using XCore.Environment.Extensions;
using XCore.Environment.Extensions.Features;

namespace XCore.DisplayManagement.Extensions
{
    public class ThemeExtensionDependencyStrategy : IExtensionDependencyStrategy
    {
        public bool HasDependency(IFeatureInfo observer, IFeatureInfo subject)
        {
            if (observer.Extension.IsTheme())
            {
                if (!subject.Extension.IsTheme())
                    return true;
            }

            return false;
        }
    }
}
