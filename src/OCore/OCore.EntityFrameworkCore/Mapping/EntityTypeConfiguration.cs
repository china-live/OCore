using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCore.Entities;

namespace OCore.EntityFrameworkCore
{
    /// <summary>
    /// Represents base entity mapping configuration
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
//    public partial class EntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class,IEntity
//    {
//        #region Utilities

//        /// <summary>
//        /// Developers can override this method in custom partial classes in order to add some custom configuration code
//        /// </summary>
//        /// <param name="builder">The builder to be used to configure the entity</param>
//        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
//        {
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Configures the entity
//        /// </summary>
//        /// <param name="builder">The builder to be used to configure the entity</param>
//        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
//        {
//            //add custom configuration
//            this.PostConfigure(builder);
//        }

//        /// <summary>
//        /// Apply this mapping configuration
//        /// </summary>
//        /// <param name="modelBuilder">The builder being used to construct the model for the database context</param>
//        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
//        {
//            try
//            {
//modelBuilder.ApplyConfiguration(this);
//            }
//            catch (System.Exception e)
//            {

//                throw e;
//            }
            
//        }

//        #endregion
//    }
}
