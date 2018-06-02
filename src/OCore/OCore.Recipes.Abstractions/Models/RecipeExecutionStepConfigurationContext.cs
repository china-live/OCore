using Newtonsoft.Json.Linq;

namespace OCore.Recipes.Models
{
    public class RecipeExecutionStepConfigurationContext : ConfigurationContext
    {
        public RecipeExecutionStepConfigurationContext(JObject configurationElement) : base(configurationElement)
        {
        }
    }
}