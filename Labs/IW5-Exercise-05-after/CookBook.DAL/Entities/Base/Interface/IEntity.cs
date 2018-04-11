using System;

namespace CookBook.DAL.Entities.Base.Interface
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}