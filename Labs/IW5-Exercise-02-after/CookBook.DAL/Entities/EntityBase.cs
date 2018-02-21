using System;

namespace CookBook.DAL.Entities
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}