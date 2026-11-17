using System.Runtime.CompilerServices;
using CookBook.Common;
using CookBook.IdentityProvider.App.Installers;
using CookBook.IdentityProvider.DAL;
using CookBook.IdentityProvider.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("CookBook.IdentityProvider.App.UnitTests")]

namespace CookBook.IdentityProvider.App.Services;

public static class LocalUserProvisioning
{
    internal sealed record Account(string Name, string Password, string? Email, string Role);

    public static async Task<(int Created, int Skipped)> ProvisionAsync(
        IConfiguration configuration,
        IReadOnlyList<(string Section, string Role)> accounts,
        bool skipExisting)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required for provisioning.");
        }

        var users = accounts.Select(account =>
        {
            var section = configuration.GetSection(account.Section);
            var name = section["UserName"];
            var password = section["Password"];
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException($"{account.Section}:UserName and Password are required.");
            }

            if (account.Role is not (UserRoles.Admin or UserRoles.User))
            {
                throw new InvalidOperationException("Provisioning role must be 'admin' or 'user'.");
            }

            return new Account(name, password, section["Email"], account.Role);
        }).ToArray();

        if (users.Select(user => user.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != users.Length)
        {
            throw new InvalidOperationException("Provisioning usernames must be distinct.");
        }

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<IdentityProviderDbContext>(options => options.UseSqlServer(connectionString));
        new IdentityProviderAppInstaller().Install(services);
        await using var provider = services.BuildServiceProvider();
        await using var scope = provider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<IdentityProviderDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUserEntity>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRoleEntity>>();

        await using var transaction = await context.Database.BeginTransactionAsync();
        var result = await ProvisionAccountsAsync(users, userManager, roleManager, skipExisting);
        await transaction.CommitAsync();
        return result;
    }

    internal static async Task<(int Created, int Skipped)> ProvisionAccountsAsync(
        IReadOnlyList<Account> users,
        UserManager<AppUserEntity> userManager,
        RoleManager<AppRoleEntity> roleManager,
        bool skipExisting)
    {
        var created = 0;
        var skipped = 0;
        foreach (var user in users)
        {
            if (await userManager.FindByNameAsync(user.Name) is not null)
            {
                if (!skipExisting)
                {
                    throw new InvalidOperationException($"User '{user.Name}' already exists; no changes were made.");
                }

                skipped++;
                continue;
            }

            if (!await roleManager.RoleExistsAsync(user.Role))
            {
                EnsureSucceeded(await roleManager.CreateAsync(new AppRoleEntity { Name = user.Role }), "create role");
            }

            var entity = new AppUserEntity
            {
                Id = Guid.NewGuid(),
                Subject = Guid.NewGuid().ToString(),
                UserName = user.Name,
                Email = user.Email,
                EmailConfirmed = false,
                Active = true
            };
            EnsureSucceeded(await userManager.CreateAsync(entity, user.Password), "create user");
            EnsureSucceeded(await userManager.AddToRoleAsync(entity, user.Role), "assign role");
            created++;
        }

        return (created, skipped);
    }

    private static void EnsureSucceeded(IdentityResult result, string operation)
    {
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Could not {operation}: {string.Join(", ", result.Errors.Select(error => error.Code))}.");
        }
    }
}
