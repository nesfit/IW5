using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Common.Models
{
    public class IngredientListModel : IWithId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

        public IngredientListModel()
        {
        }

        public IngredientListModel(Guid id, string name, string? imageUrl = null)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
