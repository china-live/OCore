using Newtonsoft.Json.Linq;
using OCore.Entities;

namespace OCore.Environment.Shell.Descriptor.Models
{
    public class ShellFeature : IEntity
    {
        public ShellFeature()
        {
        }

        public ShellFeature(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        //public JObject Properties { get; set; } = new JObject();
    }
}
