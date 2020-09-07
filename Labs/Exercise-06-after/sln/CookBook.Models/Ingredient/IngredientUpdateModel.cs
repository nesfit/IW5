using System;

namespace CookBook.Models
{
    public class IngredientUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}