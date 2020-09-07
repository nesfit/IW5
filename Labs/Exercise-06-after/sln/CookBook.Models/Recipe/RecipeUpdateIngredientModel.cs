using System;
using CookBook.Models.Enums;

namespace CookBook.Models.Recipe
{
    public class RecipeUpdateIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }
}