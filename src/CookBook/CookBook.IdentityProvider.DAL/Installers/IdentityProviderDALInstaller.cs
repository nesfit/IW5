using CookBook.Common.Installers;
using CookBook.IdentityProvider.DAL.Entities;
using CookBook.IdentityProvider.DAL.Factories;
using CookBook.IdentityProvider.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.DAL.Installers;

public class IdentityProviderDALInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbContextFactory<IdentityProviderDbContext>, IdentityProviderDbContextFactory>();

        serviceCollection.AddScoped<IUserStore<AppUserEntity>, UserStore<AppUserEntity, AppRoleEntity, IdentityProviderDbContext, Guid, AppUserClaimEntity, AppUserRoleEntity, AppUserLoginEntity, AppUserTokenEntity, AppRoleClaimEntity>>();
        serviceCollection.AddScoped<IRoleStore<AppRoleEntity>, RoleStore<AppRoleEntity, IdentityProviderDbContext, Guid, AppUserRoleEntity, AppRoleClaimEntity>>();

        serviceCollection.AddTransient<IAppUserRepository, AppUserRepository>();

        serviceCollection.AddDbContext<IdentityProviderDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required for the identity database.");
            }
            options.UseSqlServer(connectionString);
        });
    }
}
