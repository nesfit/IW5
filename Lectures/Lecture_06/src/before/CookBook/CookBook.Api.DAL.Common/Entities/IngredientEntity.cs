using System;

namespace CookBook.Api.DAL.Common.Entities
{
    public record IngredientEntity : EntityBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
