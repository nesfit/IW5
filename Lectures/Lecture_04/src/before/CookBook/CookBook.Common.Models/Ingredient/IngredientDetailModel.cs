using System;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Models.Resources.Texts;

namespace CookBook.Common.Models
{
    public record IngredientDetailModel : IWithId
    {
        public required Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(IngredientDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(IngredientDetailModelResources))]
        public required string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(IngredientDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(IngredientDetailModelResources))]
        public required string Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
