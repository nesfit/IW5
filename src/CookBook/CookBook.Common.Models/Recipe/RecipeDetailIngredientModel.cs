using System.ComponentModel.DataAnnotations;
using CookBook.Common.Enums;
using EnumsNET;

namespace CookBook.Common.Models
{
    public record RecipeDetailIngredientModel
    {
        [Range(0, double.MaxValue)]
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public IngredientListModel Ingredient { get; set; }
        public string UnitText => Unit.AsString(EnumFormat.Description)!;

        public RecipeDetailIngredientModel(double amount, Unit unit, IngredientListModel ingredient)
        {
            Amount = amount;
            Unit = unit;
            Ingredient = ingredient;
        }
    }
}
