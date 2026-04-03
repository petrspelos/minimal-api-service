using Microsoft.AspNetCore.OpenApi;
using TApi.WebApp.Transformers;

namespace TApi.WebApp.Configuration;

internal static class OpenApiConfigurator
{
    internal static void Configure(OpenApiOptions options)
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            document.Servers?.Clear();
            return Task.CompletedTask;
        });
        options.AddDocumentTransformer<KeycloakSecuritySchemeTransformer>();
        options.AddOperationTransformer<AuthOperationTransformer>();
    }
}
