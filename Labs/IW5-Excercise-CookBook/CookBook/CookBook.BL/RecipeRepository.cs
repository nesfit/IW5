using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.DAL;
using CookBook.DAL.Entities;

namespace CookBook.BL
{
    public class RecipeRepository
    {
        public RecipeEntity FindByName(string name)
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                var recipe = cookBookDbContext
                    .Recipes
                    .Include(r => r.Ingredients.Select(i => i.Ingredient))
                    .FirstOrDefault(r => r.Name == name);
                return recipe;
            }
        }
    }
}
