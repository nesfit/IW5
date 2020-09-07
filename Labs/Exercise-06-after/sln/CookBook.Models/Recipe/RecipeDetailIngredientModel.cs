using System;
using CookBook.Models.Enums;
using EnumsNET;

namespace CookBook.Models.Recipe
{
    public class RecipeDetailIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public string Name { get; set; }
        public string UnitText => Unit.AsString(EnumFormat.Description);
    }
}