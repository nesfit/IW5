using System;
using EnumsNET;

namespace CookBook.Models
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