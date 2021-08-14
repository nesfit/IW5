using CookBook.Common;
using CookBook.Common.Enums;
using EnumsNET;
using System;

namespace CookBook.Models
{
    public record RecipeListModel : IWithId
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string FoodTypeText => FoodType.AsString(EnumFormat.Description)!;

        public RecipeListModel(Guid id, string name, TimeSpan duration, FoodType foodType)
        {
            Id = id;
            Name = name;
            Duration = duration;
            FoodType = foodType;
        }
    }
}