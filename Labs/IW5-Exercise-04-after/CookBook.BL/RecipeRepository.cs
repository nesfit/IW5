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
    using CookBook.BL.Models;

    public class RecipeRepository
    {
        private readonly Mapper mapper = new Mapper();

        public RecipeDetailModel FindByName(string name)
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                var recipe = cookBookDbContext
                    .Recipes
                    .Include(r => r.Ingredients.Select(i => i.Ingredient))
                    .FirstOrDefault(r => r.Name == name);

                return this.mapper.MapDetail(recipe);
            }
        }

        public ICollection<RecipeListModel> GetAll()
        {
            using (var context = new CookBookDbContext())
            {
                return this.mapper.Map(context.Recipes.ToList());
            }
        }
        
        public RecipeDetailModel GetById(Guid id)
        {
            using (var context = new CookBookDbContext())
            {
                return this.mapper.MapDetail(context.Recipes.Find(id));
            }
        }
    }
}
