using System.Security.Claims;
using CookBook.IdentityProvider.BL.Facades;
using CookBook.IdentityProvider.BL.Models;
using Duende.IdentityModel;
using Duende.IdentityServer;
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

    public async Task GetProfileDataAsync(ProfileDataRequestContext context, CancellationToken cancellationToken = default)
    {
        var subjectId = context.Subject.GetSubjectId();

        var user = await appUserFacade.GetUserBySubjectAsync(subjectId)
            ?? await appUserFacade.GetUserByUserNameAsync(subjectId);

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
            }).Where(claim => claim is not null).Cast<Claim>().ToList();

            var provider = context.Subject.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
            var externalDisplayName = context.Subject.FindFirst(JwtClaimTypes.Name)?.Value;
            var storedDisplayName = claims
                .FirstOrDefault(claim => claim.Type == ExternalUserDisplayName.StoredClaimType)?.Value;
            claims.RemoveAll(claim => claim.Type == ExternalUserDisplayName.StoredClaimType);
            var username = user.UserName;
            // UserInfo subjects may contain only sub, without the login cookie's name/idp.
            var displayName = !string.IsNullOrWhiteSpace(provider)
                && !string.IsNullOrWhiteSpace(externalDisplayName)
                    ? externalDisplayName
                    : storedDisplayName;
            if (provider != IdentityServerConstants.LocalIdentityProvider
                && !string.IsNullOrWhiteSpace(displayName))
            {
                username = displayName;
                claims.RemoveAll(claim => claim.Type == JwtClaimTypes.Name);
                claims.Add(new Claim(JwtClaimTypes.Name, displayName));
            }
            claims.RemoveAll(claim => claim.Type == "username");
            claims.Add(new Claim("username", username));
            claims.AddRange(
                context.Subject.FindAll(JwtClaimTypes.Role)
                    .Where(externalRole => claims.All(localClaim =>
                        localClaim.Type != JwtClaimTypes.Role
                        || !string.Equals(localClaim.Value, externalRole.Value, StringComparison.Ordinal))));
            context.AddRequestedClaims(claims);
        }
    }

    public Task IsActiveAsync(IsActiveContext context, CancellationToken cancellationToken = default)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
