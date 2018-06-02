using System.Collections.Generic;
using System.Threading.Tasks;
using OCore.Recipes.Models;

namespace OCore.Recipes.Services
{
    public interface IRecipeHarvester
    {
        /// <summary>
        /// Returns a collection of all recipes.
        /// </summary>
        Task<IEnumerable<RecipeDescriptor>> HarvestRecipesAsync();

    }
}