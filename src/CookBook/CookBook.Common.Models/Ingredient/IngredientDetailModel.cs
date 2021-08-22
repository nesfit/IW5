using System;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Resources;

namespace CookBook.Common.Models
{
    public record IngredientDetailModel : IWithId
    {
        public Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(IngredientDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(IngredientDetailModelResources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(IngredientDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(IngredientDetailModelResources))]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public IngredientDetailModel()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
            ImageUrl = null;
        }

        public IngredientDetailModel(Guid id, string name, string description, string? imageUrl = null)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}
