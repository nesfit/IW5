using CookBook.Common.BL.Facades;
using CookBook.IdentityProvider.BL.Models;

namespace CookBook.IdentityProvider.BL.Facades;

public interface IAppUserClaimsFacade : IAppFacade
{
    Task<IEnumerable<AppUserClaimListModel>> GetAppUserClaimsByUserIdAsync(Guid userId);
}
