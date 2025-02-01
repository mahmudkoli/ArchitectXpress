using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace ArchitectXpress.Caching;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;

    public CacheService(
        IDistributedCache cache,
        ILogger<CacheService> logger) =>
        (_cache, _logger) = (cache, logger);

    public async Task<T> GetAsync<T>(string key, CancellationToken token = default) =>
        await GetAsync(key, token) is { } data
            ? Deserialize<T>(data)
            : default!;

    private async Task<byte[]> GetAsync(string key, CancellationToken token = default)
    {
        try
        {
            byte[]? data = await _cache.GetAsync(key, token)!;
            return data!;
        }
        catch
        {
            return default!;
        }
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        try
        {
            await _cache.RemoveAsync(key, token);
        }
        catch
        {
        }
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null, CancellationToken cancellationToken = default) =>
        SetAsync(key, Serialize(value), slidingExpiration, absoluteExpiration, cancellationToken);

    private async Task SetAsync(string key, byte[] value, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null, CancellationToken token = default)
    {
        try
        {
            await _cache.SetAsync(key, value, GetOptions(slidingExpiration, absoluteExpiration), token);
            _logger.LogDebug("Added to Cache : {key}", key);
        }
        catch
        {
        }
    }

    private byte[] Serialize<T>(T item)
    {
        return Encoding.Default.GetBytes(JsonSerializer.Serialize(item));
    }

    private T Deserialize<T>(byte[] cachedData) =>
        JsonSerializer.Deserialize<T>(Encoding.Default.GetString(cachedData));

    private static DistributedCacheEntryOptions GetOptions(TimeSpan? slidingExpiration, DateTimeOffset? absoluteExpiration)
    {
        var options = new DistributedCacheEntryOptions();
        if (slidingExpiration.HasValue)
        {
            options.SetSlidingExpiration(slidingExpiration.Value);
        }
        else
        {
            options.SetSlidingExpiration(TimeSpan.FromMinutes(10)); // Default expiration time of 10 minutes.
        }

        if (absoluteExpiration.HasValue)
        {
            options.SetAbsoluteExpiration(absoluteExpiration.Value);
        }
        else
        {
            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(15)); // Default expiration time of 10 minutes.
        }

        return options;
    }
}
