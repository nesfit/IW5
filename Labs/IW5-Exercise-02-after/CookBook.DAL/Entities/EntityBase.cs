using System;

namespace CookBook.DAL.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}