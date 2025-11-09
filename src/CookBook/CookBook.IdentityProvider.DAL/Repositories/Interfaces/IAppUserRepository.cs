using CookBook.IdentityProvider.DAL.Entities;

namespace CookBook.IdentityProvider.DAL.Repositories;

public interface IAppUserRepository
{
    Task<IList<AppUserEntity>> SearchAsync(string searchString);
}
