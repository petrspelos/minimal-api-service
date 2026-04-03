namespace TApi.WebApp.Services.Authorization;

internal sealed class CachedOpenIdConfigurationProvider(
    IOpenIdConfigurationProvider inner)
    : IOpenIdConfigurationProvider
{
    private readonly IOpenIdConfigurationProvider _inner = inner;
    private OpenIdConfiguration? _cached;
    private readonly SemaphoreSlim _lock = new(initialCount: 1, maxCount: 1);

    public async Task<OpenIdConfiguration> GetAsync(CancellationToken ct)
    {
        if (_cached is not null)
            return _cached;

        await _lock.WaitAsync(ct);
        try
        {
            _cached ??= await _inner.GetAsync(ct);
        }
        finally
        {
            _lock.Release();
        }

        return _cached;
    }
}
