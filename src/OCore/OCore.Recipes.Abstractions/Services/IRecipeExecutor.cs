using System.Threading.Tasks;
using OCore.Recipes.Models;

namespace OCore.Recipes.Services
{

    public interface IRecipeExecutor
    {
        Task<string> ExecuteAsync(string executionId, RecipeDescriptor recipeDescriptor, object environment);
    }
}