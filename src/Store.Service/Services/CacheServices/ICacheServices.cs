namespace Store.Service.Services.CacheServices;

public interface ICacheServices
{
    Task SetCacheAsync<T>(string key, T value, TimeSpan expiration);

    Task<T?> GetCacheAsync<T>(string key);
}