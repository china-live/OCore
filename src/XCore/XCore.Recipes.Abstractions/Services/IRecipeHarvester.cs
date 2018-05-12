using System.Collections.Generic;
using System.Threading.Tasks;
using XCore.Recipes.Models;

namespace XCore.Recipes.Services
{
    public interface IRecipeHarvester
    {
        /// <summary>
        /// Returns a collection of all recipes.
        /// </summary>
        Task<IEnumerable<RecipeDescriptor>> HarvestRecipesAsync();

    }
}