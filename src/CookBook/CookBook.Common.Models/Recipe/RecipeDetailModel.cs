using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.Common;
using CookBook.Common.Enums;

namespace CookBook.Models
{
    public record RecipeDetailModel : IWithId
    {
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public IList<RecipeDetailIngredientModel> IngredientAmounts { get; set; } = new List<RecipeDetailIngredientModel>();

        public RecipeDetailModel()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
            ImageUrl = null;
            Duration = TimeSpan.Zero;
            FoodType = FoodType.Unknown;
        }

        public RecipeDetailModel(Guid id, string name, string description, TimeSpan duration, FoodType foodType, string? imageUrl = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Duration = duration;
            FoodType = foodType;
            ImageUrl = imageUrl;
        }
    }
}
