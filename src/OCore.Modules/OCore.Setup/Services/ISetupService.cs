using System.Collections.Generic;
using System.Threading.Tasks;
using OCore.Recipes.Models;

namespace OCore.Setup.Services
{
    public interface ISetupService
    {
        Task<IEnumerable<RecipeDescriptor>> GetSetupRecipesAsync();
        Task<string> SetupAsync(SetupContext context);
    }
}