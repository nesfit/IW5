using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Common.Models
{
    public record IngredientListModel : IWithId
    {
        public Guid Id { get; init; }
        [Required]
        public string Name { get; set; }

        public IngredientListModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
