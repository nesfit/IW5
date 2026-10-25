using System.Security.Claims;
using CookBook.IdentityProvider.App.Services;
using CookBook.IdentityProvider.DAL.Entities;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CookBook.IdentityProvider.App.UnitTests;

public class ExternalUserDisplayNameTests
{
    [Theory]
    [InlineData("previous@example.com", true)]
    [InlineData("teacher@example.com", true)]
    [InlineData("previous@example.com", false)]
    public async Task PersistentStoreRefreshesDisplayClaimAndSurfacesWriteFailures(
        string storedName, bool succeeds)
    {
        var user = new AppUserEntity
        {
            Id = Guid.NewGuid(), Subject = "stable-subject", UserName = "local-name", Active = true
        };
        var storedClaim = new Claim("cookbook:external_display_name", storedName);
        var manager = new Mock<UserManager<AppUserEntity>>(
            Mock.Of<IUserStore<AppUserEntity>>(), null!, null!, null!, null!, null!, null!, null!, null!);
        manager.Setup(candidate => candidate.FindByLoginAsync("Entra", "tenant:object")).ReturnsAsync(user);
        manager.Setup(candidate => candidate.GetClaimsAsync(user)).ReturnsAsync(new List<Claim> { storedClaim });
        manager.Setup(candidate => candidate.GetRolesAsync(user)).ReturnsAsync(new List<string>());
        manager.Setup(candidate => candidate.ReplaceClaimAsync(user, storedClaim, It.IsAny<Claim>()))
            .ReturnsAsync(succeeds
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError { Description = "Display claim write failed" }));
        var service = new PersistentExternalUserService(
            manager.Object, new UpperInvariantLookupNormalizer(),
            NullLogger<PersistentExternalUserService>.Instance);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim("preferred_username", "teacher@example.com")], "Entra"));

        if (!succeeds)
        {
            var error = await Assert.ThrowsAsync<InvalidOperationException>(() => service.FindOrProvisionAsync(
                "Entra", "tenant:object", null, principal, CancellationToken.None));
            Assert.Contains("Display claim write failed", error.Message);
        }
        else
        {
            var result = await service.FindOrProvisionAsync(
                "Entra", "tenant:object", null, principal, CancellationToken.None);
            Assert.Equal("teacher@example.com", result.DisplayName);
            Assert.Equal("stable-subject", result.SubjectId);
        }
        manager.Verify(candidate => candidate.ReplaceClaimAsync(
            user, storedClaim, It.Is<Claim>(claim =>
                claim.Type == "cookbook:external_display_name" && claim.Value == "teacher@example.com")),
            storedName == "teacher@example.com" ? Times.Never() : Times.Once());
        manager.Verify(candidate => candidate.AddClaimAsync(user, It.IsAny<Claim>()), Times.Never);
        Assert.Equal("local-name", user.UserName);
    }

    [Theory]
    [InlineData("preferred_username", "teacher@example.com")]
    [InlineData("name", "Demo Teacher")]
    [InlineData(ClaimTypes.Name, "Demo Teacher")]
    public async Task PersistentStoreDisplaysReadableClaimWithoutRenamingExistingAccount(
        string claimType, string value)
    {
        var user = new AppUserEntity
        {
            Id = Guid.NewGuid(),
            Subject = "stable-local-subject",
            UserName = "Entra-tenant-object",
            Active = true
        };
        var manager = new Mock<UserManager<AppUserEntity>>(
            Mock.Of<IUserStore<AppUserEntity>>(), null!, null!, null!, null!, null!, null!, null!, null!);
        manager.Setup(candidate => candidate.FindByLoginAsync("Entra", "tenant:object"))
            .ReturnsAsync(user);
        manager.Setup(candidate => candidate.GetRolesAsync(user)).ReturnsAsync(new List<string>());
        manager.Setup(candidate => candidate.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>());
        manager.Setup(candidate => candidate.AddClaimAsync(user, It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);
        var service = new PersistentExternalUserService(
            manager.Object, new UpperInvariantLookupNormalizer(),
            NullLogger<PersistentExternalUserService>.Instance);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(claimType, value), new Claim("sub", "opaque-external-subject")], "Entra"));

        var result = await service.FindOrProvisionAsync(
            "Entra", "tenant:object", null, principal, CancellationToken.None);

        Assert.Equal(value, result.DisplayName);
        Assert.Equal("stable-local-subject", result.SubjectId);
        Assert.Equal("Entra-tenant-object", user.UserName);
        manager.Verify(candidate => candidate.AddClaimAsync(user, It.Is<Claim>(claim =>
            claim.Type == "cookbook:external_display_name" && claim.Value == value)), Times.Once);
        manager.Verify(candidate => candidate.UpdateAsync(It.IsAny<AppUserEntity>()), Times.Never);
    }

    [Fact]
    public async Task ReadableEmailDoesNotBecomeVerifiedEmailOrAccountLink()
    {
        AppUserEntity? createdUser = null;
        var manager = new Mock<UserManager<AppUserEntity>>(
            Mock.Of<IUserStore<AppUserEntity>>(), null!, null!, null!, null!, null!, null!, null!, null!);
        manager.Setup(candidate => candidate.FindByLoginAsync("Entra", "tenant:object"))
            .ReturnsAsync((AppUserEntity?)null);
        manager.Setup(candidate => candidate.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((AppUserEntity?)null);
        manager.Setup(candidate => candidate.CreateAsync(It.IsAny<AppUserEntity>()))
            .Callback<AppUserEntity>(user => createdUser = user)
            .ReturnsAsync(IdentityResult.Success);
        manager.Setup(candidate => candidate.AddLoginAsync(It.IsAny<AppUserEntity>(), It.IsAny<UserLoginInfo>()))
            .ReturnsAsync(IdentityResult.Success);
        manager.Setup(candidate => candidate.GetRolesAsync(It.IsAny<AppUserEntity>()))
            .ReturnsAsync(new List<string>());
        manager.Setup(candidate => candidate.GetClaimsAsync(It.IsAny<AppUserEntity>()))
            .ReturnsAsync(new List<Claim>());
        manager.Setup(candidate => candidate.AddClaimAsync(It.IsAny<AppUserEntity>(), It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);
        var service = new PersistentExternalUserService(
            manager.Object, new UpperInvariantLookupNormalizer(),
            NullLogger<PersistentExternalUserService>.Instance);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim("email", "teacher@example.com")], "Entra"));

        var result = await service.FindOrProvisionAsync(
            "Entra", "tenant:object", null, principal, CancellationToken.None);

        Assert.Equal("teacher@example.com", result.DisplayName);
        Assert.NotNull(createdUser);
        Assert.Null(createdUser.Email);
        Assert.False(createdUser.EmailConfirmed);
        Assert.Equal(createdUser.Subject, result.SubjectId);
        manager.VerifyGet(candidate => candidate.Users, Times.Never);
        manager.Verify(candidate => candidate.AddLoginAsync(
            createdUser, It.Is<UserLoginInfo>(login =>
                login.LoginProvider == "Entra" && login.ProviderKey == "tenant:object")), Times.Once);
    }

    [Theory]
    [InlineData(null, "Demo Teacher", "Demo Teacher")]
    [InlineData("   ", "Demo Teacher", "Demo Teacher")]
    [InlineData("teacher@example.com", "Demo Teacher", "Demo Teacher")]
    [InlineData(null, null, "signed-in user")]
    public async Task TestStoreSelectsReadableLabelInsteadOfOpaqueSubject(
        string? preferredUsername, string? name, string expectedName)
    {
        var claims = new List<Claim>
        {
            new("sub", "opaque-external-subject"),
            new("oid", "opaque-object")
        };
        if (preferredUsername is not null)
        {
            claims.Add(new Claim("preferred_username", preferredUsername));
        }
        if (name is not null)
        {
            claims.Add(new Claim("name", name));
        }
        var user = new TestUser
        {
            SubjectId = "stable-test-subject",
            Username = "Entra-tenant-object",
            ProviderName = "Entra",
            ProviderSubjectId = "tenant:object"
        };
        var service = new TestStoreExternalUserService(new TestUserStore([user]));

        var result = await service.FindOrProvisionAsync(
            "Entra", "tenant:object", null,
            new ClaimsPrincipal(new ClaimsIdentity(claims, "Entra")), CancellationToken.None);

        Assert.Equal(expectedName, result.DisplayName);
    }

    [Fact]
    public async Task TestStoreCombinesGivenAndFamilyNamesBeforeUsername()
    {
        var user = new TestUser
        {
            SubjectId = "stable-test-subject",
            Username = "Entra-tenant-object",
            ProviderName = "Entra",
            ProviderSubjectId = "tenant:object"
        };
        var service = new TestStoreExternalUserService(new TestUserStore([user]));
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("preferred_username", "teacher@example.com"),
                new Claim("given_name", "Demo"),
                new Claim("family_name", "Teacher")
            ], "Entra"));

        var result = await service.FindOrProvisionAsync(
            "Entra", "tenant:object", null, principal, CancellationToken.None);

        Assert.Equal("Demo Teacher", result.DisplayName);
    }

    [Fact]
    public async Task TestStoreUsesCurrentProviderNameForExistingExternalUser()
    {
        var user = new TestUser
        {
            SubjectId = "stable-test-subject",
            Username = "Entra-tenant-object",
            ProviderName = "Entra",
            ProviderSubjectId = "tenant:object"
        };
        var service = new TestStoreExternalUserService(new TestUserStore([user]));
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim("preferred_username", "teacher@example.com")], "Entra"));

        var result = await service.FindOrProvisionAsync(
            "Entra", "tenant:object", null, principal, CancellationToken.None);

        Assert.Equal("teacher@example.com", result.DisplayName);
        Assert.Equal("stable-test-subject", result.SubjectId);
        Assert.Equal("Entra-tenant-object", user.Username);
        Assert.Equal("teacher@example.com", Assert.Single(user.Claims, claim => claim.Type == "username").Value);
        Assert.Equal("teacher@example.com", Assert.Single(user.Claims, claim => claim.Type == "name").Value);
    }
}
