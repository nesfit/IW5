using System;

namespace CookBook.DAL.Entities.Base.Implementation
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}