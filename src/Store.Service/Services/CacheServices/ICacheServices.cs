namespace Store.Service.Services.CacheServices;

public interface ICacheServices
{
    /// <summary>
/// Store an object in the cache under the specified key with the given expiration duration.
/// </summary>
/// <param name="key">Cache key under which to store the response.</param>
/// <param name="response">The object to store in the cache.</param>
/// <param name="expiration">Duration after which the cached entry expires.</param>
Task SetCacheResponseAsync(string key, object response, TimeSpan expiration);

    /// <summary>
/// Retrieve the cached string value associated with the specified key.
/// </summary>
/// <param name="key">The key identifying the cached entry.</param>
/// <returns>The cached string value, or <c>null</c> if no entry exists for the key.</returns>
Task<string?> GetCacheResponseAsync(string key);
}