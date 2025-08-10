namespace Poc.App.Services;

public interface ICachingService
{
    T GetOrCreate<T>(string? cacheItem, Func<T> createItem, TimeSpan? timeExpire = null);
    Task<T> GetOrCreateAsync<T>(string? cacheKey, Func<Task<T>> createItem, TimeSpan? expireTime = null);
    void Remove(string cacheKey);
}