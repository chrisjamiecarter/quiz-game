namespace QuizGame.Application.Services;

/// <summary>
/// Provides an abstraction for cache-related operations such as retrieving, setting, and removing cached data.
/// </summary>
public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey);
    Task RemoveAsync(string cacheKey);
    Task SetAsync<T>(string cacheKey, T value);
}
