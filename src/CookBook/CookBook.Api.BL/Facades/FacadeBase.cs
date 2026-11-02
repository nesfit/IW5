using CookBook.Api.BL.Models;
using CookBook.Api.DAL.Common.Entities.Interfaces;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.BL.Facades;

public class FacadeBase<TRepository, TEntity>(
    TRepository repository)
    where TRepository : IApiRepository<TEntity>
    where TEntity : IEntity
{
    public ResourceOwnership? GetOwnership(Guid id)
        => repository.GetById(id) is { } entity
            ? new ResourceOwnership(entity.Id, entity.OwnerId)
            : null;
}
