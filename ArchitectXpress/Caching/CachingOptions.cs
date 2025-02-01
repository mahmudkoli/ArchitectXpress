namespace ArchitectXpress.Caching;

public class CachingOptions
{
    public int SlidingExpirationInMinutes { get; set; } = 2;
    public int AbsoluteExpirationInMinutes { get; set; } = 5;
    public string? RedisURL { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
