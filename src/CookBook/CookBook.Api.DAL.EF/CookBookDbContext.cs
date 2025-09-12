using CookBook.Api.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF
{
    public class CookBookDbContext : DbContext
    {
        public DbSet<IngredientEntity> Ingredients { get; set; } = null!;
        public DbSet<RecipeEntity> Recipes { get; set; } = null!;
        public DbSet<IngredientAmountEntity> IngredientAmounts { get; set; } = null!;

        public CookBookDbContext(DbContextOptions<CookBookDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(recipeEntity => recipeEntity.IngredientAmounts)
                .WithOne(ingredientAmountEntity => ingredientAmountEntity.Recipe)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngredientEntity>()
                .HasMany(typeof(IngredientAmountEntity))
                .WithOne(nameof(IngredientAmountEntity.Ingredient))
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
