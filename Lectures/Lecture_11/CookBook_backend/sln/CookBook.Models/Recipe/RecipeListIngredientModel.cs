using EnumsNET;
using System;

namespace CookBook.Models
{
    public class RecipeListIngredientModel
    {
        public IngredientListModel Ingredient { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public string UnitText => Unit.AsString(EnumFormat.Description);
    }
}