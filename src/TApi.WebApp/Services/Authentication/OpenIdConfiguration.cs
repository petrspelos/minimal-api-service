namespace TApi.WebApp.Services.Authentication;

internal sealed class OpenIdConfiguration
{
    public required string TokenEndpoint { get; init; }

    public required IReadOnlyList<string> Scopes { get; init; }
}
