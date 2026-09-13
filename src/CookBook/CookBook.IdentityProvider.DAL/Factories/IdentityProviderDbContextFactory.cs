using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CookBook.IdentityProvider.DAL.Factories;

public class IdentityProviderDbContextFactory : IDesignTimeDbContextFactory<IdentityProviderDbContext>, IDbContextFactory<IdentityProviderDbContext>
{
    private readonly Assembly startupAssembly;

    public IdentityProviderDbContextFactory()
    {
        startupAssembly = Assembly.GetEntryAssembly()!;
    }

    public IdentityProviderDbContext CreateDbContext(string[] args)
        => CreateDbContext();

    public IdentityProviderDbContext CreateDbContext()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddUserSecrets<IdentityProviderDbContextFactory>(optional: true)
            .AddUserSecrets(startupAssembly, optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<IdentityProviderDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        return new IdentityProviderDbContext(optionsBuilder.Options);
    }
}
