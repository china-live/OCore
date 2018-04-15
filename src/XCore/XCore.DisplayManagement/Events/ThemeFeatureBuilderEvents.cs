using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCore.DisplayManagement.Extensions;
using XCore.DisplayManagement.Manifest;
using XCore.Environment.Extensions.Features;
using XCore.Modules.Manifest;

namespace XCore.DisplayManagement.Events
{
    public class ThemeFeatureBuilderEvents : FeatureBuilderEvents
    {
        public override void Building(FeatureBuildingContext context)
        {
            var moduleInfo = context.ExtensionInfo.Manifest.ModuleInfo;

            if (moduleInfo is ThemeAttribute || (moduleInfo is ModuleMarkerAttribute &&
                moduleInfo.Type.Equals("Theme", StringComparison.OrdinalIgnoreCase)))
            {
                var extensionInfo = new ThemeExtensionInfo(context.ExtensionInfo);

                if (extensionInfo.HasBaseTheme())
                {
                    context.FeatureDependencyIds = context
                        .FeatureDependencyIds
                        .Concat(new[] { extensionInfo.BaseTheme })
                        .ToArray();
                }

                context.ExtensionInfo = extensionInfo;
            }
        }
    }
}
