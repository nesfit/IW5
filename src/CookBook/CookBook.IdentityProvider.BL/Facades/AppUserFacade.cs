using AutoMapper;
using CookBook.Common.Models.User;
using CookBook.IdentityProvider.BL.Models;
using CookBook.IdentityProvider.DAL.Entities;
using CookBook.IdentityProvider.DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CookBook.IdentityProvider.BL.Facades;

public class AppUserFacade(
    UserManager<AppUserEntity> userManager,
    IAppUserRepository appUserRepository,
    IMapper mapper) : IAppUserFacade
{
    public async Task<Guid?> CreateAppUserAsync(AppUserCreateModel appUserModel)
    {
        if (await userManager.FindByNameAsync(appUserModel.UserName) is not null)
        {
            throw new ArgumentException($"User with username '{appUserModel.UserName}' already exists");
        }

        var appUserEntity = mapper.Map<AppUserEntity>(appUserModel);
        await userManager.CreateAsync(appUserEntity, appUserModel.Password);
        return (await userManager.FindByNameAsync(appUserModel.UserName))?.Id;
    }

    public async Task<bool> ValidateCredentialsAsync(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }
        else
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
    }

    public async Task<Guid> GetUserIdByUserNameAsync(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        return user.Id;
    }

    public async Task<AppUserDetailModel?> GetUserByIdAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if(user is null)
        {
            return null;
        }

        return mapper.Map<AppUserDetailModel>(user);
    }

    public async Task<AppUserDetailModel?> GetUserByUserNameAsync(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user is null)
        {
            return null;
        }

        return mapper.Map<AppUserDetailModel>(user);
    }

    public async Task<AppUserDetailModel?> GetAppUserByExternalProviderAsync(string provider, string providerIdentityKey)
    {
        var user = userManager.FindByLoginAsync(provider, providerIdentityKey);
        return mapper.Map<AppUserDetailModel>(user);
    }

    public async Task<AppUserDetailModel> CreateExternalAppUserAsync(AppUserExternalCreateModel appUserModel)
    {
        var appUserEntity = new AppUserEntity
        {
            Id = Guid.NewGuid(),
            Active = true,
            Subject = Guid.NewGuid().ToString(),
            Email = appUserModel.Email,
        };

        var userLoginInfo = new UserLoginInfo(appUserModel.Provider, appUserModel.ProviderIdentityKey, null);

        await userManager.CreateAsync(appUserEntity);
        await userManager.AddLoginAsync(appUserEntity, userLoginInfo);

        var createdAppUserEntity = await userManager.FindByNameAsync(appUserEntity.UserName);

        return mapper.Map<AppUserDetailModel>(createdAppUserEntity);
    }

    public async Task<bool> ActivateUserAsync(string securityCode, string email)
    {
        var appUserEntity = await userManager.FindByEmailAsync(email);
        var identityResult = await userManager.ConfirmEmailAsync(appUserEntity, securityCode);

        return identityResult.Succeeded;
    }

    public async Task<bool> IsEmailConfirmedAsync(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }
        else
        {

            return await userManager.IsEmailConfirmedAsync(user);
        }
    }

    public async Task<IList<AppUserListModel>> SearchAsync(string searchString)
    {
        var users = await appUserRepository.SearchAsync(searchString);
        return mapper.Map<List<AppUserListModel>>(users);
    }
}
