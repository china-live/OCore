using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XCore.EntityFrameworkCore;
using XCore.Recipes.Entitys;
using XCore.Recipes.Models;
using XCore.Recipes.Services;

namespace OrchardCore.Recipes.Services
{
    /// <summary>
    /// And implementation of <see cref="IRecipeStore"/> that stores the recipe<see cref="RecipeResult"/>
    /// object in YesSql.
    /// </summary>
    public class RecipeStore : IRecipeStore
    {
        //private readonly ISession _session;
        public AppDbContext Context { get; private set; }
        private DbSet<Recipe> RecipeSet { get { return Context.Set<Recipe>(); } }
        public bool AutoSaveChanges { get; set; } = true;
        public virtual IQueryable<Recipe> Recipes
        {
            get { return RecipeSet; }
        }

        public RecipeStore(AppDbContext context)
        {
            //_session = session;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected Task SaveChanges()
        {
            return AutoSaveChanges ? Context.SaveChangesAsync() : Task.CompletedTask;
        }

        public async Task CreateAsync(RecipeResult recipeResult)
        {
            //_session.Save(recipeResult);
            await RecipeSet.AddAsync(new Recipe() { ExecutionId = recipeResult.ExecutionId, JsonValue = JsonConvert.SerializeObject(recipeResult) });
            await SaveChanges();
        }

        public async Task DeleteAsync(RecipeResult recipeResult)
        {
            //_session.Delete(recipeResult);
            var model = await RecipeSet.SingleOrDefaultAsync(c => c.Id == recipeResult.Id);
            if (model != null)
            {
                RecipeSet.Remove(model);
                await SaveChanges();
            }
        }

        public async Task<RecipeResult> FindByExecutionIdAsync(string executionId)
        {
            var json = await Recipes.FirstOrDefaultAsync(c => c.ExecutionId == executionId);
            if (json != null)
            {
                return JsonConvert.DeserializeObject<RecipeResult>(json.JsonValue);
            }
            return null ;
        }

        public async Task UpdateAsync(RecipeResult recipeResult)
        {
            var model = await RecipeSet.SingleOrDefaultAsync(c => c.ExecutionId == recipeResult.ExecutionId);
            if (model != null)
            {
                //model.ExecutionId = recipeResult.ExecutionId;
                model.JsonValue = JsonConvert.SerializeObject(recipeResult);
                RecipeSet.Update(model);
                await SaveChanges();
            }
        }
    }
}
