using System.Security.Claims;
using CookBook.IdentityProvider.BL.Facades;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace CookBook.IdentityProvider.App.Services;

public class LocalAppUserProfileService : IProfileService
{
    private readonly IAppUserFacade appUserFacade;
    private readonly IAppUserClaimsFacade appUserClaimsFacade;

    public LocalAppUserProfileService(
        IAppUserFacade appUserFacade,
        IAppUserClaimsFacade appUserClaimsFacade)
    {
        this.appUserFacade = appUserFacade;
        this.appUserClaimsFacade = appUserClaimsFacade;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userName = context.Subject.GetSubjectId();
        var user = await appUserFacade.GetUserByUserNameAsync(userName);
        if (user is not null)
        {
            var appUserClaims = await appUserClaimsFacade.GetAppUserClaimsByUserIdAsync(user.Id);
            var claims = appUserClaims.Select(claim =>
            {
                if (claim.ClaimType is not null
                    && claim.ClaimValue is not null)
                {
                    return new Claim(claim.ClaimType, claim.ClaimValue);
                }
                return null;
            }).ToList();
            context.AddRequestedClaims(claims);
        }

    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
    }
}
