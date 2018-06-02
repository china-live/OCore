using System.Collections.Generic;

namespace OCore.Environment.Extensions
{
    public class ManifestOptions 
    {
        public IList<ManifestOption> ManifestConfigurations { get; }
            = new List<ManifestOption>();
    }
}