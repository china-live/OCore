using System.Threading.Tasks;
using XCore.Recipes.Models;

namespace XCore.Recipes.Services
{
    /// <summary>
    /// An implementation of this interface will be used everytime a recipe step is processed.
    /// Each implementation is reponsible for processing only the steps that it targets.
    /// 每次处理配方（recipe）步骤时都会使用此接口的实现。
    /// 每个实现都只负责处理它所针对的步骤。
    /// </summary>
    public interface IRecipeStepHandler
    {
        /// <summary>
        /// Processes a recipe step.
        /// </summary>
        /// <param name="context">The context object representing the processed step.</param>
        Task ExecuteAsync(RecipeExecutionContext context);
    }
}