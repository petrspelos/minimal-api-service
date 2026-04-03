namespace TApi.WebApp.Services.Authorization;

internal interface IOpenIdConfigurationProvider
{
    Task<OpenIdConfiguration> GetAsync(CancellationToken cancellationToken = default);
}
