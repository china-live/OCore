using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCore.DisplayManagement.Extensions;
using XCore.Environment.Extensions.Features;

namespace XCore.DisplayManagement.Events
{
    public class ThemeFeatureBuilderEvents : FeatureBuilderEvents
    {
        public override void Building(FeatureBuildingContext context)
        {
            if (context.ExtensionInfo.Manifest.IsTheme())
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
