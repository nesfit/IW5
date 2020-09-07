using CookBook.Models;
using System.Collections.Generic;

namespace CookBook.Web.MVC.ViewModels.Recipe
{
    public class RecipeNewViewModel
    {
        public IList<IngredientListModel> IngredientsAll { get; set; }
        public RecipeNewModel RecipeNewModel { get; set; }
        public string DurationText { get; set; }
    }
}
