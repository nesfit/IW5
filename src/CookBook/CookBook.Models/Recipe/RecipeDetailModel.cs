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
        public IList<RecipeDetailIngredientModel> IngredientAmounts { get; set; }

        public RecipeDetailModel(Guid id, string name, string description, TimeSpan duration, FoodType foodType, string? imageUrl = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Duration = duration;
            FoodType = foodType;
            ImageUrl = imageUrl;
            IngredientAmounts = new List<RecipeDetailIngredientModel>();
        }
    }
}
