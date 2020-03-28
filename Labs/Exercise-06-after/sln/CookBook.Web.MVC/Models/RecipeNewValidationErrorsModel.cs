using System.Collections.Generic;

namespace CookBook.Web.MVC.Models
{
    public class RecipeNewValidationErrorsModel
    {
        public IList<string> Name { get; set; }
        public IList<string> Duration { get; set; }
        public IList<string> FoodTzpe { get; set; }
        public IList<string> Description { get; set; }
        public IList<string> Ingredients { get; set; }
    }
}