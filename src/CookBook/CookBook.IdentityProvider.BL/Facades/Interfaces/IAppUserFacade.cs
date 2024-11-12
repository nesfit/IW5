using CookBook.Common.BL.Facades;
using CookBook.IdentityProvider.BL.Models;
using CookBook.IdentityProvider.DAL.Entities;

namespace CookBook.IdentityProvider.BL.Facades;

public interface IAppUserFacade : IAppFacade
{
    Task<AppUserEntity?> CreateAppUserAsync(AppUserCreateModel appUserModel);
    Task<bool> ValidateCredentialsAsync(string userName, string password);
    Task<Guid> GetUserIdByUserNameAsync(string userName);
    public Task<AppUserDetailModel?> GetUserByIdAsync(Guid id);
    Task<AppUserDetailModel?> GetUserByUserNameAsync(string userName);
    Task<AppUserDetailModel?> GetAppUserByExternalProviderAsync(string provider, string providerIdentityKey);
    Task<AppUserDetailModel> CreateExternalAppUserAsync(AppUserExternalCreateModel appUserModel);
    Task<bool> ActivateUserAsync(string securityCode, string email);
    Task<bool> IsEmailConfirmedAsync(string userName);
    Task<string> CreateAppUserAndGenerateEmailConfirmationTokenAsync(AppUserCreateModel appUserModel);
}
