using System;

namespace CookBook.Models
{
    public class RecipeListIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }
}