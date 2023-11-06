using CookBook.IdentityProvider.BL.Models;

namespace CookBook.IdentityProvider.BL.Facades;

public interface IAppUserClaimsFacade
{
    Task<IEnumerable<AppUserClaimListModel>> GetAppUserClaimsBySubjectAsync(string subject);
}
