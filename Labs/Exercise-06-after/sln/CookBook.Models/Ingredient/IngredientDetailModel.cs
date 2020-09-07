using System;

namespace CookBook.Models.Ingredient
{
    public class IngredientDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}