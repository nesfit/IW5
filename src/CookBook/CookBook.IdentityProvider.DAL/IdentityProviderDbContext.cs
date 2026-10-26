using CookBook.IdentityProvider.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookBook.IdentityProvider.DAL;

public class IdentityProviderDbContext : IdentityDbContext<AppUserEntity, AppRoleEntity, Guid, AppUserClaimEntity, AppUserRoleEntity, AppUserLoginEntity, AppRoleClaimEntity, AppUserTokenEntity>
{
    public IdentityProviderDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Subject is the token `sub` and resolves users in the profile service, so it must be unique.
        builder.Entity<AppUserEntity>()
            .HasIndex(user => user.Subject)
            .IsUnique();
    }
}
