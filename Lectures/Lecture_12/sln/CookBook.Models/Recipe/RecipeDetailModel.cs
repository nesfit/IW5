using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public class RecipeDetailModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string ImageUrl { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeDetailIngredientModel> Ingredients { get; set; }
    }
}