using System.Threading.Tasks;
using XCore.Recipes.Models;

namespace XCore.Recipes.Services
{

    public interface IRecipeExecutor
    {
        Task<string> ExecuteAsync(string executionId, RecipeDescriptor recipeDescriptor, object environment);
    }
}