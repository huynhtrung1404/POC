using Microsoft.Extensions.Caching.Memory;
using Poc.App.Services;

namespace Poc.Api.Services;

public class CatchingService(IMemoryCache memoryCache) : ICachingService
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    public T GetOrCreate<T>(string? cacheItem, Func<T> createItem, TimeSpan? expireTime = null)
    {
        if (string.IsNullOrWhiteSpace(cacheItem))
            throw new NullReferenceException();
        if (!_memoryCache.TryGetValue(cacheItem, out T? cacheEntry))
        {
            cacheEntry = createItem();
            var cacheOption = new MemoryCacheEntryOptions();
            if (expireTime.HasValue)
            {
                cacheOption.SetAbsoluteExpiration(expireTime.Value);
            }
            _memoryCache.Set(cacheItem, cacheEntry, cacheOption);
        }
        return cacheEntry!;
    }

    public async Task<T> GetOrCreateAsync<T>(string? cacheKey, Func<Task<T>> createItem, TimeSpan? expireTime = null)
    {
        if (string.IsNullOrWhiteSpace(cacheKey))
            throw new NullReferenceException();
        if (!_memoryCache.TryGetValue(cacheKey, out T? cacheEntry))
        {
            // Key not in cache, so create data
            cacheEntry = await createItem();

            var cacheEntryOptions = new MemoryCacheEntryOptions();

            if (expireTime.HasValue)
            {
                cacheEntryOptions.SetAbsoluteExpiration(expireTime.Value);
            }

            _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
        }

        return cacheEntry!;
    }

    public void Remove(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }
}