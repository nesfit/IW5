using System.Security.Claims;
using CookBook.IdentityProvider.BL.Facades;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace CookBook.IdentityProvider.App.Services;

public class LocalAppUserProfileService : IProfileService
{
    private readonly IAppUserClaimsFacade appUserClaimsFacade;

    public LocalAppUserProfileService(IAppUserClaimsFacade appUserClaimsFacade)
    {
        this.appUserClaimsFacade = appUserClaimsFacade;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subjectId = context.Subject.GetSubjectId();

        var appUserClaims = await appUserClaimsFacade.GetAppUserClaimsBySubjectAsync(subjectId);
        var claims = appUserClaims.Select(claim => new Claim(claim.ClaimType, claim.ClaimValue)).ToList();
        context.AddRequestedClaims(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
    }
}
