using CookBook.Api.DAL.Common.Entities.Interfaces;
using CookBook.Api.DAL.Common.Repositories;
using System;

namespace CookBook.Api.BL.Facades;

public class FacadeBase<TRepository, TEntity>
    where TRepository : IApiRepository<TEntity>
    where TEntity : IEntity
{
    private readonly TRepository repository;

    public FacadeBase(TRepository repository)
    {
        this.repository = repository;
    }

    protected void ThrowIfWrongOwner(Guid id, string? ownerId)
    {
        if (ownerId is not null
            && repository.GetById(id)?.OwnerId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }
    }
}

