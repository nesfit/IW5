using CookBook.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public record IngredientDetailModel : IId
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public IngredientDetailModel(Guid id, string name, string description, string? imageUrl = null)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}