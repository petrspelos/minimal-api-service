namespace TApi.WebApp.Services.Authentication;

internal interface IOpenIdConfigurationProvider
{
    Task<OpenIdConfiguration> GetAsync(CancellationToken cancellationToken = default);
}
