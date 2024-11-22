using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using QuizGame.Application.Options;
using QuizGame.Application.Services;

namespace QuizGame.Infrastructure.Services;

/// <summary>
/// Implements a cache service using in-memory caching to store and retrieve data with expiration policies.
/// </summary>
internal class DatabaseCacheService : ICacheService
{
    private readonly int _cacheExpiration;
    private readonly IMemoryCache _memoryCache;

    public DatabaseCacheService(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions)
    {
        _cacheExpiration = cacheOptions.Value.ExpirationInMinutes;
        _memoryCache = memoryCache;
    }

    public Task<T?> GetAsync<T>(string cacheKey)
    {
        if (_memoryCache.TryGetValue(cacheKey, out T? value))
        {
            return Task.FromResult(value);
        }

        return Task.FromResult(default(T?));
    }

    public Task RemoveAsync(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string cacheKey, T value)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = GetCacheExpiration(),
        };

        _memoryCache.Set(cacheKey, value, options);
        return Task.CompletedTask;
    }

    private TimeSpan GetCacheExpiration()
    {
        return TimeSpan.FromMinutes(_cacheExpiration);
    }
}
