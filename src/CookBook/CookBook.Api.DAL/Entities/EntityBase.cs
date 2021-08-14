using System;

namespace CookBook.Api.DAL.Entities
{
    public abstract record EntityBase : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        protected EntityBase()
        {
        }

        protected EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
