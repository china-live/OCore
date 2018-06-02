using Microsoft.Extensions.DependencyInjection;
using OCore.Recipes.Services;

namespace OCore.Recipes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRecipeExecutionStep<TImplementation>(
            this IServiceCollection serviceCollection)
            where TImplementation : class, IRecipeStepHandler
        {
            serviceCollection.AddScoped<IRecipeStepHandler, TImplementation>();

            return serviceCollection;
        }
    }
}