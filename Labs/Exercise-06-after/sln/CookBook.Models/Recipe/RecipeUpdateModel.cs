using System;
using System.Collections.Generic;
using CookBook.Models.Enums;

namespace CookBook.Models.Recipe
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