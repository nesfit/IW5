using System.Collections.Generic;
using CookBook.Models.Ingredient;
using CookBook.Models.Recipe;

namespace CookBook.Web.MVC.ViewModels.Recipe
{
    public class RecipeNewViewModel
    {
        public IList<IngredientListModel> IngredientsAll { get; set; }
        public RecipeNewModel RecipeNewModel { get; set; }
        public string DurationText { get; set; }
    }
}
