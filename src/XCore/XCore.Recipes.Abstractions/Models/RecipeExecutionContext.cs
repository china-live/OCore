using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace XCore.Recipes.Models
{
    public class RecipeExecutionContext
    {
        public string ExecutionId { get; set; }
        public object Environment { get; set; }
        public string Name { get; set; }
        public JObject Step { get; set; }
        public RecipeDescriptor RecipeDescriptor { get; set; }
        public IEnumerable<RecipeDescriptor> InnerRecipes { get; set; }
    }
}