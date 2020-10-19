using CookBook.Models;
using EnumsNET;

namespace CookBook.BL.Api.Models.Recipe
{
    public class RecipeDetailIngredientModel
    {
        public IngredientListModel Ingredient { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public string UnitText => Unit.AsString(EnumFormat.Description);
    }
}