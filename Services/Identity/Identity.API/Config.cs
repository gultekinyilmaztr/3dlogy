using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity.API;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
           new ApiResource("ResourceCatalog"){Scopes={"CatalogFullPermission","CatalogReadPermission"} },
           new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
          new IdentityResources.OpenId(),
          new IdentityResources.Profile(),
          new IdentityResources.Email()
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
            new ApiScope("CatalogFullPermission","Full authority for catalog operations"),
            new ApiScope("CatalogReadPermission","Reading authority for catalog operations"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<Client> Clients => new Client[]
    {
            //Visitor
            new Client
            {
                ClientId="VisitorClient",
                ClientName="Visitor Client",
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("DuendeVisitorClientSecret".Sha256())},
                AllowedScopes={ "CatalogReadPermission",  IdentityServerConstants.LocalApi.ScopeName },
                AllowAccessTokensViaBrowser=true
            },

            //Admin
            new Client
            {
                ClientId="AdminClient",
                ClientName="Admin Client",
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                ClientSecrets={new Secret("DuendeAdminClientSecret".Sha256()) },
                AllowedScopes={"CatalogFullPermission", "CatalogReadPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime=600
            }
    };
}

