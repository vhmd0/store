using Microsoft.Extensions.Caching.Hybrid;

namespace Store.Service.Services.CacheServices;

public class CacheServices : ICacheServices
{
    private readonly HybridCache _hybridCache;

    /// <summary>
    /// Initializes a new instance of <see cref="CacheServices"/> using the provided <see cref="HybridCache"/>.
    /// </summary>
    /// <param name="hybridCache">The hybrid cache instance used for caching operations.</param>
    public CacheServices(HybridCache hybridCache)
    {
        _hybridCache = hybridCache;
    }
    /// <summary>
    /// Stores the given object in the hybrid cache under the specified key with the provided expiration.
    /// </summary>
    /// <remarks>
    /// If <paramref name="response"/> is null, the method performs no cache operation.
    /// </remarks>
    /// <param name="key">Cache key identifying the entry.</param>
    /// <param name="response">Value to store in the cache.</param>
    /// <param name="expiration">Duration after which the cached entry expires.</param>
    public async Task SetCacheResponseAsync(string key, object response, TimeSpan expiration)
    {
        if (response is null)
            return;

        await _hybridCache.SetAsync(key, response, new HybridCacheEntryOptions { Expiration = expiration });
    }

    /// <summary>
    /// Retrieves the cached string value for the specified cache key.
    /// </summary>
    /// <param name="key">The cache key to look up.</param>
    /// <returns>The cached string value if present; otherwise <c>null</c>.</returns>
    public async Task<string?> GetCacheResponseAsync(string key)
    {
        return await _hybridCache.GetOrCreateAsync<string?>(key, _ => ValueTask.FromResult<string?>(null));
    }
}