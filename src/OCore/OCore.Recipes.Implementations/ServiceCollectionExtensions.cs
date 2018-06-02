using Microsoft.Extensions.DependencyInjection;
using OCore.Recipes.Services;

namespace OCore.Recipes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRecipes(this IServiceCollection services)
        {
            services.AddScoped<IRecipeHarvester, ApplicationRecipeHarvester>();
            services.AddScoped<IRecipeHarvester, RecipeHarvester>();
            services.AddScoped<IRecipeExecutor, RecipeExecutor>();

            return services;
        }
    }
}
