using System.Collections.Generic;

namespace XCore.Environment.Extensions
{
    public class ManifestOptions 
    {
        public IList<ManifestOption> ManifestConfigurations { get; }
            = new List<ManifestOption>();
    }
}