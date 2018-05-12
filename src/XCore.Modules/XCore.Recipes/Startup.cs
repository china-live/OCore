using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Recipes.RecipeSteps;
using OrchardCore.Recipes.Services;
using System;
using XCore.Modules;
using XCore.Recipes.Services;

namespace XCore.Recipes
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
