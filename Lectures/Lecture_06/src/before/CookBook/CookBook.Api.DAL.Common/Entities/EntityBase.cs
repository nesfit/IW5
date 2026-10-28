using System;
using CookBook.Api.DAL.Common.Entities.Interfaces;

namespace CookBook.Api.DAL.Common.Entities
{
    public abstract record EntityBase : IEntity
    {
        public required Guid Id { get; init; }
        public string? OwnerId { get; set; }
    }
}
