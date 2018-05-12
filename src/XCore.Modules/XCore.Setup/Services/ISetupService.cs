using System.Collections.Generic;
using System.Threading.Tasks;
using XCore.Recipes.Models;

namespace XCore.Setup.Services
{
    public interface ISetupService
    {
        Task<IEnumerable<RecipeDescriptor>> GetSetupRecipesAsync();
        Task<string> SetupAsync(SetupContext context);
    }
}