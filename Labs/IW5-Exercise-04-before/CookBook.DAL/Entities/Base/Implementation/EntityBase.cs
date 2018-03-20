using System;
using CookBook.DAL.Entities.Base.Interface;

namespace CookBook.DAL.Entities.Base.Implementation
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}