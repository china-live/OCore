using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCore.EntityFrameworkCore;
using OCore.Recipes.Entitys;

namespace OCore.Recipes.EntityFrameworkCore
{
    public class RecipeMap : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> a)
        {
            a.ToTable("Recipe");
            a.HasKey(r => r.Id);
            //a.Ignore(r => r.Properties);
        }
    }
}
