using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Enums;
using CookBook.Common.Models.Resources.Texts;

namespace CookBook.Common.Models
{
    public record RecipeDetailModel : IWithId
    {
        public required Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(RecipeDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(RecipeDetailModelResources))]
        public required string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(RecipeDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(RecipeDetailModelResources))]
        [MinLength(10, ErrorMessageResourceName = nameof(RecipeDetailModelResources.Description_MinLength_ErrorMessage), ErrorMessageResourceType = typeof(RecipeDetailModelResources))]
        public required string Description { get; set; }

        public string? ImageUrl { get; set; }
        public required TimeSpan Duration { get; set; }
        public required FoodType FoodType { get; set; }
        public IList<RecipeDetailIngredientModel> IngredientAmounts { get; set; } = new List<RecipeDetailIngredientModel>();
    }
}
