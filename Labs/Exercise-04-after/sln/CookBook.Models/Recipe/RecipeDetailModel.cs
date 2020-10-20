using CookBook.Models;
using System;
using System.Collections.Generic;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeDetailIngredientModel> Ingredients { get; set; }
    }
}