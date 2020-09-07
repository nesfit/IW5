using System.Collections.Generic;
using CookBook.Models.Recipe;

namespace CookBook.Web.MVC.ViewModels.Recipe
{
    public class RecipeListViewModel
    {
        public IList<RecipeListModel> Recipes { get; set; }
    }
}
