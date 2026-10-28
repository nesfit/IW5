using CookBook.IdentityProvider.BL.Facades;
using Duende.IdentityServer.Validation;
using Duende.IdentityModel;

namespace CookBook.IdentityProvider.App.Services;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly IAppUserFacade appUserFacade;

    public ResourceOwnerPasswordValidator(IAppUserFacade appUserFacade)
    {
        this.appUserFacade = appUserFacade;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context, CancellationToken cancellationToken = default)
    {
        var areCredentialsValid = await appUserFacade.ValidateCredentialsAsync(context.UserName, context.Password);

        if (areCredentialsValid
            && await appUserFacade.GetUserByUserNameAsync(context.UserName) is { } user)
        {
            // Same `sub` as interactive login (Login page, profile service): the stable Subject, not the database id.
            context.Result = new GrantValidationResult(user.Subject, OidcConstants.AuthenticationMethods.Password);
        }
    }
}
