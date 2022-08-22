using System;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Enums;

namespace CookBook.Common.Models
{
    public record RecipeDetailIngredientModel
    {
        public required Guid Id { get; set; }

        [Range(0, double.MaxValue)]
        public required double Amount { get; set; }
        public required Unit Unit { get; set; }
        public required IngredientListModel Ingredient { get; set; }
    }
}
