using CookBook.DAL.Entities;
using System.Data.Entity;

namespace CookBook.DAL
{
    public class CookBookDbContext : DbContext
    {
        public IDbSet<RecipeEntity> Recipes { get; set; }
        public IDbSet<IngredientEntity> Ingredients { get; set; }

        public CookBookDbContext() : base("LocalDb")
        {
        }

        public override int SaveChanges()
        {
            //FixAttachedEntities();
            return base.SaveChanges();
        }

        //private void FixAttachedEntities()
        //{
        //    // We don't want to reInsert ingredients with existing ID so we need to attach them to db context
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        if (entry.Entity is IEntity)
        //        {
        //            var intEntity = (IEntity)entry.Entity;
        //            if (intEntity.Id != Guid.Empty && (entry.State == EntityState.Detached || entry.State == EntityState.Added))
        //            {
        //                Set(intEntity.GetType()).Attach(intEntity);
        //                Entry(intEntity).State = EntityState.Modified;
        //            }
        //        }
        //    }
        //}
    }
}