using CookBook.Common;
using CookBook.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public class RecipeDetailModel : IId
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeDetailIngredientModel> Ingredients { get; set; } = new List<RecipeDetailIngredientModel>();
    }
}