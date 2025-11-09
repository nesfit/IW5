using CookBook.Api.DAL.Common.Entities.Interfaces;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common;

namespace CookBook.Api.BL.Facades;

public class FacadeBase<TRepository, TEntity>(
    TRepository repository)
    where TRepository : IApiRepository<TEntity>
    where TEntity : IEntity
{
    protected void ThrowIfWrongOwnerAndNotAdmin(Guid? id, IList<string> userRoles, string? ownerId)
    {
        if (id is not null
            && userRoles is not null
            && !userRoles.Contains(UserRoles.Admin)
            && ownerId is not null
            && repository.GetById(id.Value)?.OwnerId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }
    }
}
