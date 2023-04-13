using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace SocialNetwork.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("SocialNetworkWebApi","Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("SocialNetworkWebApi","Web API", new[] { JwtClaimTypes.Name })
                {
                    Scopes = { "SocialNetworkWebApi" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "social-network-web-api",
                    ClientName = "Social Network",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://.../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "SocialNetworkWebApi"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
