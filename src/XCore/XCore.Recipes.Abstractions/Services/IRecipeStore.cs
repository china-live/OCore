using System.Threading.Tasks;
using XCore.Recipes.Models;

namespace XCore.Recipes.Services
{
    public interface IRecipeStore
    {
        Task CreateAsync(RecipeResult recipeResult);
        
        Task DeleteAsync(RecipeResult recipeResult);
        
        Task<RecipeResult> FindByExecutionIdAsync(string executionId);

        Task UpdateAsync(RecipeResult recipeResult);
    }
}
