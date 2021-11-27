using System;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;

namespace CookBook.Common.Models
{
    public class RecipeListModel : IWithId
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string FoodTypeDisplayText => FoodType.GetReadableName();
        public string? ImageUrl { get; set; }

    }
}
