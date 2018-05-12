using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.Entities;

namespace XCore.Recipes.Entitys
{
    public class Recipe : IEntity
    {
        public JObject Properties { get; set; } = new JObject();

        public virtual int Id { get; set; }

        public virtual string ExecutionId { get; set; }

        public virtual string JsonValue { get; set; }
    }
}
