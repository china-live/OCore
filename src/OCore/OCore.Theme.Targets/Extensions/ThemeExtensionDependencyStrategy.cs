using OCore.Environment.Extensions;
using OCore.Environment.Extensions.Features;

namespace OCore.Theme
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
