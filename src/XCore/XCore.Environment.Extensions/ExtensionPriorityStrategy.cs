using XCore.Environment.Extensions.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Environment.Extensions
{
    public class ExtensionPriorityStrategy : IExtensionPriorityStrategy
    {
        public int GetPriority(IFeatureInfo feature)
        {
            return feature.Priority;
        }
    }
}
