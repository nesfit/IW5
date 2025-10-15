using CookBook.Api.DAL.Common.Entities.Interfaces;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.Api.BL.Facades;

public class FacadeBase<TRepository, TEntity>(
    TRepository repository)
    where TRepository : IApiRepository<TEntity>
    where TEntity : IEntity
{
    protected void ThrowIfWrongOwner(Guid? id, string? ownerId)
    {
        if (id is not null
            && ownerId is not null
            && repository.GetById(id.Value)?.OwnerId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }
    }
}
