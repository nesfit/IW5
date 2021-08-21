using System;
using CookBook.Common.Enums;
using EnumsNET;

namespace CookBook.Common.Models
{
    public record RecipeListModel : IWithId
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string FoodTypeText => FoodType.AsString(EnumFormat.Description)!;
        public string? ImageUrl { get; set; }

        public RecipeListModel(Guid id, string name, TimeSpan duration, FoodType foodType, string? imageUrl = null)
        {
            Id = id;
            Name = name;
            Duration = duration;
            FoodType = foodType;
            ImageUrl = imageUrl;
        }
    }
}
