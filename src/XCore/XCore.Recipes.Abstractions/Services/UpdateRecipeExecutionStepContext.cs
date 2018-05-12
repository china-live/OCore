using Newtonsoft.Json.Linq;

namespace XCore.Recipes.Services
{
    public class UpdateRecipeExecutionStepContext
    {
        public JObject RecipeDocument { get; set; }
        public JObject Step { get; set; }
    }
}