using Microsoft.AspNetCore.OpenApi;

namespace TApi.WebApp.Configuration;

internal static class OpenApiConfigurator
{
    internal static void ConfigureOpenApi(OpenApiOptions options)
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            document.Servers?.Clear();
            return Task.CompletedTask;
        });
    }
}
