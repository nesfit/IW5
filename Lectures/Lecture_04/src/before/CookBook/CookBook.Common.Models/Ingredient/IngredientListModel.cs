using System;

namespace CookBook.Common.Models
{
    public record IngredientListModel : IWithId
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
