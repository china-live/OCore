using Newtonsoft.Json.Linq;
using OCore.Entities;

namespace OCore.Environment.Shell.Descriptor.Models
{
    /// <summary>
    /// A shell parameter is a custom value that can be assigned to a specific component in a shell.
    /// </summary>
    public class ShellParameter : IEntity
    {
        public int Id { get; set; }
        public string Component { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        //public JObject Properties { get; set; } = new JObject();
    }
}
