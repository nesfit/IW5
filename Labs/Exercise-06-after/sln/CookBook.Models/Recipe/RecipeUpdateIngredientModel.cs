using System;

namespace CookBook.Models
{
    public class RecipeUpdateIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }
}