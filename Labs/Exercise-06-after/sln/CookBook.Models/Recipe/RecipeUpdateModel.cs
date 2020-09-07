using System;
using System.Collections.Generic;

namespace CookBook.Models
{
    public class RecipeUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeUpdateIngredientModel> Ingredients { get; set; }
    }
}