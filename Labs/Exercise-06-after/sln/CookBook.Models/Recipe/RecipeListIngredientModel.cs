using EnumsNET;
using System;

namespace CookBook.Models
{
    public class RecipeListIngredientModel
    {
        public string Name { get; set; }
        public Guid IngredientId { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public string UnitText => Unit.AsString(EnumFormat.Description);
    }
}