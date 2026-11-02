using System;
using CookBook.Common.Enums;

namespace CookBook.Common.Models
{
    public record RecipeListModel : IWithId
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
        public required TimeSpan Duration { get; set; }
        public required FoodType FoodType { get; set; }
        public string? ImageUrl { get; set; }
    }
}
