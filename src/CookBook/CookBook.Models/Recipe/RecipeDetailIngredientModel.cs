using CookBook.Common.Enums;
using EnumsNET;
using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public class RecipeDetailIngredientModel
    {
        public IngredientListModel Ingredient { get; set; } = new IngredientListModel();

        [Range(0, Double.MaxValue)]
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public string UnitText => Unit.AsString(EnumFormat.Description);
    }
}