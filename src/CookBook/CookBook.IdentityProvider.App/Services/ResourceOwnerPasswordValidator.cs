using CookBook.IdentityProvider.BL.Facades;
using Duende.IdentityServer.Validation;
using IdentityModel;

namespace CookBook.IdentityProvider.App.Services;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly IAppUserFacade appUserFacade;

    public ResourceOwnerPasswordValidator(IAppUserFacade appUserFacade)
    {
        this.appUserFacade = appUserFacade;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var areCredentialsValid = await appUserFacade.ValidateCredentialsAsync(context.UserName, context.Password);

        if (areCredentialsValid)
        {
            var userId = await appUserFacade.GetUserIdByUserNameAsync(context.UserName);

            context.Result = new GrantValidationResult(userId.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
