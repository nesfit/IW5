using System.Security.Claims;
using CookBook.IdentityProvider.App.Services;
using CookBook.IdentityProvider.BL.Facades;
using CookBook.IdentityProvider.BL.Models;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Moq;
using Xunit;

namespace CookBook.IdentityProvider.App.UnitTests;

public class ExternalProfileDisplayNameTests
{
    [Theory]
    [InlineData(null, "teacher@example.com")]
    [InlineData("Entra", "teacher@example.com")]
    [InlineData(IdentityServerConstants.LocalIdentityProvider, "local-teacher")]
    public async Task ProfileCanResolveDisplayNameWithoutLoginCookieClaims(string? provider, string expected)
    {
        var user = new AppUserDetailModel
        {
            Id = Guid.NewGuid(), Subject = "stable-local-subject", UserName = "local-teacher"
        };
        var users = new Mock<IAppUserFacade>();
        users.Setup(facade => facade.GetUserBySubjectAsync(user.Subject)).ReturnsAsync(user);
        var claims = new Mock<IAppUserClaimsFacade>();
        claims.Setup(facade => facade.GetAppUserClaimsByUserIdAsync(user.Id)).ReturnsAsync(
            [new AppUserClaimListModel
            {
                ClaimType = "cookbook:external_display_name", ClaimValue = "teacher@example.com"
            }]);
        var service = new LocalAppUserProfileService(users.Object, claims.Object);
        var subjectClaims = new List<Claim> { new("sub", user.Subject) };
        if (provider is not null)
        {
            subjectClaims.Add(new Claim("idp", provider));
        }
        var context = new ProfileDataRequestContext(
            new ClaimsPrincipal(new ClaimsIdentity(subjectClaims, "access-token")),
            new Client(), "UserInfoEndpoint", ["username", "name", "cookbook:external_display_name"]);

        await service.GetProfileDataAsync(context);

        Assert.Equal(expected, Assert.Single(context.IssuedClaims, claim => claim.Type == "username").Value);
        Assert.DoesNotContain(context.IssuedClaims, claim => claim.Type == "cookbook:external_display_name");
    }

    [Theory]
    [InlineData("Entra", "teacher@example.com")]
    [InlineData(IdentityServerConstants.LocalIdentityProvider, "Entra-tenant-object")]
    [InlineData("", "Entra-tenant-object")]
    public async Task ProfileUsesExternalSessionNameOnlyForExternalLogin(string provider, string expectedUsername)
    {
        var user = new AppUserDetailModel
        {
            Id = Guid.NewGuid(),
            Subject = "stable-local-subject",
            UserName = "Entra-tenant-object"
        };
        var users = new Mock<IAppUserFacade>();
        users.Setup(facade => facade.GetUserBySubjectAsync(user.Subject)).ReturnsAsync(user);
        var claims = new Mock<IAppUserClaimsFacade>();
        claims.Setup(facade => facade.GetAppUserClaimsByUserIdAsync(user.Id))
            .ReturnsAsync(Array.Empty<AppUserClaimListModel>());
        var service = new LocalAppUserProfileService(users.Object, claims.Object);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("sub", user.Subject),
                new Claim("idp", provider),
                new Claim("name", "teacher@example.com")
            ], "cookie"));
        var context = new ProfileDataRequestContext(
            principal, new Client(), "test", ["username", "name"]);

        await service.GetProfileDataAsync(context);

        Assert.Equal(expectedUsername, Assert.Single(context.IssuedClaims, claim => claim.Type == "username").Value);
        if (provider == "Entra")
        {
            Assert.Equal("teacher@example.com",
                Assert.Single(context.IssuedClaims, claim => claim.Type == "name").Value);
        }
    }
}
