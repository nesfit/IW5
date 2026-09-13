using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityModel;

namespace CookBook.IdentityProvider.App
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources
        {
            get
            {
                var profileIdentityResources = new IdentityResources.Profile();
                profileIdentityResources.UserClaims.Add("username");
                
                return
                [
                    new IdentityResources.OpenId(),
                    profileIdentityResources
                ];
            }
        }

        public static IEnumerable<ApiResource> ApiResources =>
        [
            new ("cookbookclientaudience")
        ];

        public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ("cookbookapi", [JwtClaimTypes.Role])
        ];

        public static IEnumerable<Client> Clients =>
        [
            new()
                {
                    ClientName = "CookBook Client",
                    ClientId = "cookbookclient",
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    [
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "cookbookapi"
                    ],
                    // Option 1 - use for Client Credentials and Resource Owner Password demonstration
                    //AllowedGrantTypes =
                    //[
                    //    GrantType.ClientCredentials,
                    //    GrantType.ResourceOwnerPassword,
                    //],
                    //ClientSecrets =
                    //{
                    //    new Secret("secret".Sha256())
                    //},
                    //RequirePkce = false,
                    //RequireClientSecret = true,

                    // Option 2 - use for final deployment with PKCE and no client secret
                    AllowedGrantTypes =
                    [
                        GrantType.AuthorizationCode
                    ],
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris =
                    [
                        "https://oauth.pstmn.io/v1/callback",
                        "https://localhost:44355/authentication/login-callback",
                    ],
                    PostLogoutRedirectUris =
                    [
                        "https://localhost:44355"
                    ],
                }
        ];
    }
}
