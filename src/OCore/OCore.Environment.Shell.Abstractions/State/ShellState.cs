using Newtonsoft.Json.Linq;
using OCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCore.Environment.Shell.State
{
    /// <summary>
    /// Represents the transitive list of features a tenant is made of at a specific moment.
    /// It's used to differentiate new features from existing ones in order to trigger events like
    /// Installed/Unistalled compared to only Enabled/Disabled.
    /// </summary>
    public class ShellState: IEntity
    {
        //public JObject Properties { get; set; } = new JObject();
        public int Id { get; set; }
        public List<ShellFeatureState> Features { get; set; } = new List<ShellFeatureState>();
    }
}
