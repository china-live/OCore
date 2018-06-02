using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using OCore.Recipes.Entitys;

namespace OCore.Recipes.EntityFrameworkCore
{
    public class RecipeEntityTypeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> a)
        {
            a.ToTable("Recipe");
            a.HasKey(r => r.Id);
            a.Ignore(r => r.Properties);
        }
    }
}
