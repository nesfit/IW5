using CookBook.DAL.Entities.Base.Interface;
using System;

namespace CookBook.DAL.Entities.Base.Implementation
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}