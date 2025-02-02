namespace ArchitectXpress.Caching;

public static class CacheServiceExtensions
{
    public static async Task<T?> GetOrSetAsync<T>(this ICacheService cache, string key, Func<Task<T>> getItemCallback, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null, CancellationToken cancellationToken = default)
    {
        T? value = await cache.GetAsync<T>(key, cancellationToken);

        if (value is not null)
        {
            return value;
        }

        value = await getItemCallback();

        if (value is not null)
        {
            await cache.SetAsync(key, value, slidingExpiration, absoluteExpiration, cancellationToken);
        }

        return value;
    }
}
