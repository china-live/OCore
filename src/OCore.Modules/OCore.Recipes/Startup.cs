using Microsoft.Extensions.DependencyInjection;
using OCore.Modules;
using OCore.Recipes.RecipeSteps;
using OCore.Recipes.Services;

namespace OCore.Recipes
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddRecipes();

            services.AddScoped<IRecipeStore, RecipeStore>().AddEntityFrameworkSqlServer();

            //services.AddSingleton<IIndexProvider, RecipeResultIndexProvider>();
            //services.AddScoped<IDataMigration, Migrations>();

            //services.AddRecipeExecutionStep<CommandStep>();
            services.AddRecipeExecutionStep<RecipesStep>();
        }
    }
}
