using CookBook.BL.Web.MVC.Api;
using System.Collections.Generic;

namespace CookBook.Web.MVC.ViewModels.Recipe
{
    public class RecipeListViewModel
    {
        public IList<RecipeListModel> Recipes { get; set; }
    }
}
