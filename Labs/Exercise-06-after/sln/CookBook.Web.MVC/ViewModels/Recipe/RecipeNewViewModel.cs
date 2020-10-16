using CookBook.BL.Web.MVC.Api;
using System.Collections.Generic;

namespace CookBook.Web.MVC.ViewModels.Recipe
{
    public class RecipeNewViewModel
    {
        public IList<IngredientListModel> IngredientsAll { get; set; }
        public RecipeDetailModel RecipeModel { get; set; }
        public string DurationText { get; set; }
    }
}
