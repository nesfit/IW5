using System;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Enums;

namespace CookBook.Common.Models
{
    public record RecipeDetailIngredientModel
    {
        public Guid? Id { get; set; }

        [Range(0, double.MaxValue)]
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public IngredientListModel Ingredient { get; set; }

        public RecipeDetailIngredientModel(Guid? id, double amount, Unit unit, IngredientListModel ingredient)
        {
            Id = id;
            Amount = amount;
            Unit = unit;
            Ingredient = ingredient;
        }
    }
}
