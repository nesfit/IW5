using CookBook.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public record IngredientListModel : IId
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IngredientListModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}