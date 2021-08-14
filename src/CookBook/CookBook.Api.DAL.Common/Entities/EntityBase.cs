using System;
using CookBook.Api.DAL.Common.Entities.Interfaces;

namespace CookBook.Api.DAL.Common.Entities
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
