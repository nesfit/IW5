using System;

namespace CookBook.DAL.Api.Entities
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}