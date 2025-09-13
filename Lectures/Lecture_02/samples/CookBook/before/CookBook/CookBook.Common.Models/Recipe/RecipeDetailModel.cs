using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Enums;
using CookBook.Common.Models.Resources.Texts;

namespace CookBook.Common.Models
{
    public record RecipeDetailModel : IWithId
    {
        public Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(RecipeDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(RecipeDetailModelResources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(RecipeDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(RecipeDetailModelResources))]
        [MinLength(10, ErrorMessageResourceName = nameof(RecipeDetailModelResources.Description_MinLength_ErrorMessage), ErrorMessageResourceType = typeof(RecipeDetailModelResources))]
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
