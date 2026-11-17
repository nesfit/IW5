using CookBook.IdentityProvider.App.Services;
using CookBook.IdentityProvider.DAL.Entities;
using CookBook.IdentityProvider.DAL;
using CookBook.IdentityProvider.DAL.Installers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CookBook.IdentityProvider.App.UnitTests;

public class LocalUserProvisioningTests
{
    [Fact]
    public async Task CreatesUsersWithRequestedRolesAndUnconfirmedEmails()
    {
        var users = CreateUserManager();
        var roles = CreateRoleManager();
        var created = new List<AppUserEntity>();
        users.Setup(manager => manager.CreateAsync(It.IsAny<AppUserEntity>(), It.IsAny<string>()))
            .Callback<AppUserEntity, string>((user, _) => created.Add(user))
            .ReturnsAsync(IdentityResult.Success);
        users.Setup(manager => manager.AddToRoleAsync(It.IsAny<AppUserEntity>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        roles.Setup(manager => manager.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        roles.Setup(manager => manager.CreateAsync(It.IsAny<AppRoleEntity>())).ReturnsAsync(IdentityResult.Success);

        var result = await LocalUserProvisioning.ProvisionAccountsAsync(
            [new("teacher", "admin-password", "teacher@example.com", "admin"),
             new("student", "user-password", null, "user")], users.Object, roles.Object, false);

        Assert.Equal((2, 0), result);
        Assert.Equal(2, created.Count);
        Assert.All(created, user =>
        {
            Assert.True(user.Active);
            Assert.False(user.EmailConfirmed);
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.True(Guid.TryParse(user.Subject, out _));
        });
        Assert.NotEqual(created[0].Subject, created[1].Subject);
        users.Verify(manager => manager.AddToRoleAsync(created[0], "admin"), Times.Once);
        users.Verify(manager => manager.AddToRoleAsync(created[1], "user"), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExistingUsersAreNeverModified(bool skipExisting)
    {
        var users = CreateUserManager();
        var roles = CreateRoleManager();
        users.Setup(manager => manager.FindByNameAsync("existing"))
            .ReturnsAsync(new AppUserEntity { UserName = "existing" });
        var accounts = new[] { new LocalUserProvisioning.Account("existing", "unused", null, "admin") };

        if (skipExisting)
        {
            Assert.Equal((0, 1), await LocalUserProvisioning.ProvisionAccountsAsync(
                accounts, users.Object, roles.Object, true));
        }
        else
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                LocalUserProvisioning.ProvisionAccountsAsync(accounts, users.Object, roles.Object, false));
        }

        users.Verify(manager => manager.CreateAsync(It.IsAny<AppUserEntity>(), It.IsAny<string>()), Times.Never);
        users.Verify(manager => manager.AddToRoleAsync(It.IsAny<AppUserEntity>(), It.IsAny<string>()), Times.Never);
        roles.Verify(manager => manager.CreateAsync(It.IsAny<AppRoleEntity>()), Times.Never);
    }

    [Fact]
    public async Task IdentityErrorsAreReportedWithoutIncludingPassword()
    {
        var users = CreateUserManager();
        var roles = CreateRoleManager();
        roles.Setup(manager => manager.RoleExistsAsync("admin")).ReturnsAsync(true);
        users.Setup(manager => manager.CreateAsync(It.IsAny<AppUserEntity>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "PasswordRejected", Description = "secret" }));

        var error = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            LocalUserProvisioning.ProvisionAccountsAsync(
                [new("teacher", "secret", null, "admin")], users.Object, roles.Object, false));
        Assert.Contains("PasswordRejected", error.Message);
        Assert.DoesNotContain("secret", error.Message);
    }

    [Fact]
    public async Task RejectsMissingSettingsBeforeConnecting()
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["ConnectionStrings:DefaultConnection"] = "unused",
            ["ProvisionUser:UserName"] = "teacher"
        }).Build();

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            LocalUserProvisioning.ProvisionAsync(configuration, [("ProvisionUser", "admin")], false));
    }

    [Fact]
    public void RuntimeContextUsesApplicationConfiguration()
    {
        const string connectionString = "Server=localhost;Database=identity-test;User Id=test;Password=test;TrustServerCertificate=True";
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["ConnectionStrings:DefaultConnection"] = connectionString
        }).Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        new IdentityProviderDALInstaller().Install(services);
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IdentityProviderDbContext>();

        Assert.Equal("identity-test", context.Database.GetDbConnection().Database);
        Assert.Equal("localhost", context.Database.GetDbConnection().DataSource);
        Assert.Same(context, scope.ServiceProvider.GetRequiredService<IdentityProviderDbContext>());
    }

    private static Mock<UserManager<AppUserEntity>> CreateUserManager() =>
        new(Mock.Of<IUserStore<AppUserEntity>>(), null!, null!, null!, null!, null!, null!, null!, null!);

    private static Mock<RoleManager<AppRoleEntity>> CreateRoleManager() =>
        new(Mock.Of<IRoleStore<AppRoleEntity>>(), null!, null!, null!, null!);
}
