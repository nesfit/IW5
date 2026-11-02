using CookBook.IdentityProvider.App;
using Xunit;

namespace CookBook.IdentityProvider.App.UnitTests;

public class ApiResourceConfigurationTests
{
    [Fact]
    public void CookBookApiScopeSelectsTheAudienceTrustedByTheApi()
    {
        var client = Assert.Single(Config.Clients, client => client.ClientId == "cookbookclient");
        Assert.Contains("cookbookapi", client.AllowedScopes);
        Assert.Contains(Config.ApiScopes, scope => scope.Name == "cookbookapi");

        var resources = Config.ApiResources
            .Where(resource => resource.Scopes.Contains("cookbookapi"))
            .ToList();

        Assert.Contains(resources, resource => resource.Name == "cookbookclientaudience");
    }
}
