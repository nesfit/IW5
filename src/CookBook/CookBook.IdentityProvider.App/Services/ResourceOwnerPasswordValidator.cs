using CookBook.IdentityProvider.DAL.Entities;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace CookBook.IdentityProvider.App.Services;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<AppUserEntity> userManager;

    public ResourceOwnerPasswordValidator(UserManager<AppUserEntity> userManager)
    {
        this.userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await userManager.FindByNameAsync(context.UserName);
        if (user is not null
            && await userManager.CheckPasswordAsync(user, context.Password))
        {
            context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
