using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using TApi.WebApp.Services.Authentication;

namespace TApi.WebApp.Transformers;

internal sealed class KeycloakSecuritySchemeTransformer(
    IOpenIdConfigurationProvider provider)
    : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var openId = await provider.GetAsync(cancellationToken);

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["Keycloak"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                ClientCredentials = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri(openId.TokenEndpoint),
                    Scopes = openId.Scopes.ToDictionary(
                        scope => scope,
                        scope => scope
                    )
                }
            }
        };
    }
}
