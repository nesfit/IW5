using CookBook.IdentityProvider.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookBook.IdentityProvider.DAL.Repositories;

public class AppUserRepository(IdentityProviderDbContext identityProviderDbContext) : IAppUserRepository
{
    public async Task<IList<AppUserEntity>> SearchAsync(string searchString)
        => await identityProviderDbContext.Users.Where(user => EF.Functions.Like(user.UserName, $"%{searchString}%"))
        .ToListAsync();
}
