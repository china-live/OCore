using System;
using System.Collections.Generic;
using XCore.Features.Models;
using XCore.Environment.Extensions;

namespace XCore.Features.ViewModels 
{
    public class FeaturesViewModel 
    {
        public IEnumerable<ModuleFeature> Features { get; set; }
        public FeaturesBulkAction BulkAction { get; set; }
        public Func<IExtensionInfo, bool> IsAllowed { get; set; }
    }

    public enum FeaturesBulkAction 
    {
        None,
        Enable,
        Disable,
        Update,
        Toggle
    }
}