using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using OCore.Entities;

namespace OCore.Recipes.Entitys
{
    public class Recipe : IEntity
    {
        //public JObject Properties { get; set; } = new JObject();

        public virtual int Id { get; set; }

        public virtual string ExecutionId { get; set; }

        public virtual string JsonValue { get; set; }
    }
}
