using Microsoft.Extensions.Caching.Hybrid;

namespace Store.Service.Services.CacheServices;

public class CacheServices : ICacheServices
{
    private readonly HybridCache _hybridCache;

    public CacheServices(HybridCache hybridCache)
    {
        _hybridCache = hybridCache;
    }

    public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiration)
    {
        if (value is null)
            return;

        await _hybridCache.SetAsync(key, value, new HybridCacheEntryOptions { Expiration = expiration });
    }

    public async Task<T?> GetCacheAsync<T>(string key)
    {
        return await _hybridCache.GetOrCreateAsync<T?>(key, _ => ValueTask.FromResult(default(T?)));
    }
}