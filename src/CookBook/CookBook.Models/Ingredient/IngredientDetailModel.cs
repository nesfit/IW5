using CookBook.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public record IngredientDetailModel : IWithId
    {
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
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
