using System.Collections.Generic;
using CookBook.Models.Ingredient;

namespace CookBook.Web.MVC.ViewModels.Ingredient
{
    public class IngredientListViewModel
    {
        public IList<IngredientListModel> Ingredients { get; set; }
    }
}
