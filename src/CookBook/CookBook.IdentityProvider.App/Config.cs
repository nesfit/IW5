using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace CookBook.IdentityProvider.App
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ("cookbookclientaudience"),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new("cookbookapi")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new()
                {
                    ClientName = "CookBook Client",
                    ClientId = "cookbookclient",
                    AllowOfflineAccess = true,
                    RedirectUris = new List<string>
                    {
                        "https://oauth.pstmn.io/v1/callback",
                        "https://localhost:44355/authentication/login-callback",
                    },
                    AllowedGrantTypes = new List<string>
                    {
                        GrantType.ClientCredentials,
                        GrantType.ResourceOwnerPassword,
                        GrantType.AuthorizationCode
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "cookbookapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireClientSecret = false
                }
            };
    }
}
