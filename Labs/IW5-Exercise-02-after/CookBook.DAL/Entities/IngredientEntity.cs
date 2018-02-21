using System;

namespace CookBook.DAL.Entities
{
    public class IngredientEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
