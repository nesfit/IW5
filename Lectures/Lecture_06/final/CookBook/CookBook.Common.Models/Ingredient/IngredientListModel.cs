using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Common.Models
{
    public record IngredientListModel : IWithId
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

        public IngredientListModel(Guid id, string name, string? imageUrl = null)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
