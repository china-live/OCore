using System.Threading.Tasks;
using OCore.Recipes.Models;

namespace OCore.Recipes.Services
{
    public interface IRecipeStore
    {
        Task CreateAsync(RecipeResult recipeResult);
        
        Task DeleteAsync(RecipeResult recipeResult);
        
        Task<RecipeResult> FindByExecutionIdAsync(string executionId);

        Task UpdateAsync(RecipeResult recipeResult);
    }
}
