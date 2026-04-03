using System.Text.Json;

namespace TApi.WebApp.Services.Authorization;

internal sealed class OpenIdConfigurationProvider(
    HttpClient httpClient,
    IConfiguration config)
    : IOpenIdConfigurationProvider
{
    private readonly HttpClient _httpClient = httpClient;

    private readonly string _metadataAddress = config["Keycloak:MetadataAddress"]
            ?? throw new InvalidOperationException("Missing Keycloak metadata address.");

    public async Task<OpenIdConfiguration> GetAsync(CancellationToken cancellationToken = default)
    {
        var json = await _httpClient.GetStringAsync(_metadataAddress, cancellationToken);

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var tokenEndpoint = root.GetProperty("token_endpoint").GetString()
            ?? throw new InvalidOperationException("Missing token_endpoint.");

        var scopes = root.TryGetProperty("scopes_supported", out var scopesElement)
            ? scopesElement.EnumerateArray()
                .Select(x => x.GetString()!)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray()
            : [];

        return new OpenIdConfiguration
        {
            TokenEndpoint = tokenEndpoint,
            Scopes = scopes
        };
    }
}
