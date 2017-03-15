using System;
using CookBook.DAL.Entities;
using System.Data.Entity;

namespace CookBook.DAL
{
    public class CookBookDbContext : DbContext
    {
        public IDbSet<RecipeEntity> Recipes { get; set; }
        public IDbSet<IngredientEntity> Ingredients { get; set; }

        public CookBookDbContext()
            : base("CookBookContext")
        {
        }
    }
}