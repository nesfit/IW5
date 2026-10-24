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

        return Task.FromResult(new ExternalUser(
            user.SubjectId,
            user.Username,
            user.Claims.ToList()));
    }
}
