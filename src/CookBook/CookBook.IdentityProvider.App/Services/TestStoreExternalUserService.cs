using System.Security.Claims;
using Duende.IdentityServer.Test;
using Duende.IdentityModel;

namespace CookBook.IdentityProvider.App.Services;

public sealed class TestStoreExternalUserService(TestUserStore users) : IExternalUserService
{
    public Task<ExternalUser> FindOrProvisionAsync(
        string provider,
        string providerSubject,
        string? verifiedEmail,
        ClaimsPrincipal externalPrincipal,
        CancellationToken cancellationToken)
    {
        var user = users.FindByExternalProvider(provider, providerSubject);
        if (user is null)
        {
            var claims = externalPrincipal.Claims
                .Where(claim => claim.Type is not JwtClaimTypes.Subject and not ClaimTypes.NameIdentifier)
                .ToList();
            user = users.AutoProvisionUser(provider, providerSubject, claims);
        }

        var displayName = ExternalUserDisplayName.GetDisplayName(externalPrincipal);
        user.Claims = user.Claims
            .Where(claim => claim.Type is not "username" and not JwtClaimTypes.Name)
            .Concat([new Claim("username", displayName), new Claim(JwtClaimTypes.Name, displayName)])
            .ToList();

        return Task.FromResult(new ExternalUser(
            user.SubjectId,
            displayName,
            user.Claims.ToList()));
    }
}
