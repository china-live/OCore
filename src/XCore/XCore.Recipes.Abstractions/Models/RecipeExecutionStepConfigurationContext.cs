using Newtonsoft.Json.Linq;

namespace XCore.Recipes.Models
{
    public class RecipeExecutionStepConfigurationContext : ConfigurationContext
    {
        public RecipeExecutionStepConfigurationContext(JObject configurationElement) : base(configurationElement)
        {
        }
    }
}