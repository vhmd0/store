using Microsoft.Extensions.Caching.Hybrid;

namespace Store.Service.Services.CacheServices;

public class CacheServices : ICacheServices
{
    private readonly HybridCache _hybridCache;

    public CacheServices(HybridCache hybridCache)
    {
        _hybridCache = hybridCache;
    }
    public async Task SetCacheResponseAsync(string key, object response, TimeSpan expiration)
    {
        if (response is null)
            return;

        await _hybridCache.SetAsync(key, response, new HybridCacheEntryOptions { Expiration = expiration });
    }

    public async Task<string?> GetCacheResponseAsync(string key)
    {
        return await _hybridCache.GetOrCreateAsync<string?>(key, _ => ValueTask.FromResult<string?>(null));
    }
}