using System.Collections.Generic;

namespace CookBook.Web.MVC.Models
{
    public class RecipeNewValidationErrorsModel
    {
        public IList<string> Name { get; set; } = new List<string>();
        public IList<string> Duration { get; set; } = new List<string>();
        public IList<string> FoodType { get; set; } = new List<string>();
        public IList<string> Description { get; set; } = new List<string>();
        public IList<string> Ingredients { get; set; } = new List<string>();
    }
}