using CookBook.Common;
using CookBook.Common.Enums;
using EnumsNET;
using System;

namespace CookBook.Models
{
    public class RecipeListModel : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string FoodTypeText => FoodType.AsString(EnumFormat.Description);
    }
}