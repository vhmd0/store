namespace Store.Service.Services.CacheServices;

public interface ICacheServices
{
    Task SetCacheResponseAsync(string key, object response, TimeSpan expiration);

    Task<string?> GetCacheResponseAsync(string key);
}