using Microsoft.EntityFrameworkCore;
using System;

namespace XCore.EntityFrameworkCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IEntityManager entityManager)
            : base(options)
        {
            this.entityManager = entityManager;
        }

        private IEntityManager entityManager;
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var type in entityManager.GetEntitys())
            {
                if (builder.Model.FindEntityType(type) == null)//防止重复附加模型，否则会在生成指令中报错
                {
                    builder.Model.AddEntityType(type);
                }
            }

            foreach (var type in entityManager.GetEntityTypeConfiguration())
            {
                dynamic mappingInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(mappingInstance);
            }

            base.OnModelCreating(builder);
        }
    }
}
