﻿namespace ArchitectXpress.Caching;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key, CancellationToken token = default);
    Task RemoveAsync(string key, CancellationToken token = default);
    Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null, CancellationToken cancellationToken = default);
}