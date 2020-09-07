using System;
using CookBook.Models.Enums;
using EnumsNET;

namespace CookBook.Models.Recipe
{
    public class RecipeListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string FoodTypeText => FoodType.AsString(EnumFormat.Description);
    }
}