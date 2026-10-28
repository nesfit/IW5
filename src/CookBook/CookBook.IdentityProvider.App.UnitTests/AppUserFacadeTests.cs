using System.Security.Claims;
using CookBook.IdentityProvider.BL.Facades;
using CookBook.IdentityProvider.BL.Mappers;
using CookBook.IdentityProvider.BL.Models;
using CookBook.IdentityProvider.DAL.Entities;
using CookBook.IdentityProvider.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace CookBook.IdentityProvider.App.UnitTests;

public class AppUserFacadeTests
{
    [Theory]
    [InlineData(" Demo Teacher ", "Demo Teacher")]
    [InlineData(null, null)]
    [InlineData("   ", null)]
    public async Task CreateStoresEnteredNameAsDisplayClaim(string? enteredName, string? expectedClaim)
    {
        AppUserEntity? createdUser = null;
        var manager = new Mock<UserManager<AppUserEntity>>(
            Mock.Of<IUserStore<AppUserEntity>>(), null!, null!, null!, null!, null!, null!, null!, null!);
        manager.SetupSequence(candidate => candidate.FindByNameAsync("teacher"))
            .ReturnsAsync((AppUserEntity?)null)
            .ReturnsAsync(() => createdUser);
        manager.Setup(candidate => candidate.CreateAsync(It.IsAny<AppUserEntity>(), "password"))
            .Callback<AppUserEntity, string>((user, _) => createdUser = user)
            .ReturnsAsync(IdentityResult.Success);
        manager.Setup(candidate => candidate.AddClaimAsync(It.IsAny<AppUserEntity>(), It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);
        var facade = new AppUserFacade(manager.Object, Mock.Of<IAppUserRepository>(), new AppUserMapper());

        await facade.CreateAppUserAsync(new AppUserCreateModel
        {
            UserName = "teacher",
            Password = "password",
            Email = "teacher@example.com",
            Name = enteredName
        });

        Assert.True(Guid.TryParse(createdUser!.Subject, out _));
        Assert.True(createdUser.Active);

        manager.Verify(candidate => candidate.AddClaimAsync(It.IsAny<AppUserEntity>(), It.IsAny<Claim>()),
            expectedClaim is null ? Times.Never() : Times.Once());
        if (expectedClaim is not null)
        {
            manager.Verify(candidate => candidate.AddClaimAsync(createdUser!, It.Is<Claim>(claim =>
                claim.Type == "name" && claim.Value == expectedClaim)), Times.Once());
        }
    }

    [Fact]
    public async Task CreateGeneratesUniqueSubjectInsteadOfUsingClientSubject()
    {
        var createdUsers = new List<AppUserEntity>();
        var manager = new Mock<UserManager<AppUserEntity>>(
            Mock.Of<IUserStore<AppUserEntity>>(), null!, null!, null!, null!, null!, null!, null!, null!);
        manager.Setup(candidate => candidate.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((string userName) => createdUsers.SingleOrDefault(user => user.UserName == userName));
        manager.Setup(candidate => candidate.CreateAsync(It.IsAny<AppUserEntity>(), "password"))
            .Callback<AppUserEntity, string>((user, _) => createdUsers.Add(user))
            .ReturnsAsync(IdentityResult.Success);
        var facade = new AppUserFacade(manager.Object, Mock.Of<IAppUserRepository>(), new AppUserMapper());

        foreach (var userName in new[] { "alice", "bob" })
        {
            await facade.CreateAppUserAsync(new AppUserCreateModel
            {
                UserName = userName,
                Password = "password",
                Email = $"{userName}@example.com"
            });
        }

        Assert.Equal(2, createdUsers.Select(user => user.Subject).Distinct().Count());
        Assert.DoesNotContain(createdUsers, user => user.Subject == "shared-subject");
    }
}
