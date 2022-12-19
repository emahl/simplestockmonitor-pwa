namespace SimpleStockMonitorPWA.Services;

public class PersistentStateService
{
    private const int DefaultExpirationTimeSeconds = 300;

    private readonly Dictionary<string, (DateTime CreatedAt, object CachedValue)> _cache;
    private readonly int _expirationTimeSeconds;

    public PersistentStateService(Dictionary<string, (DateTime, object)> cache, int expirationTimeSeconds = DefaultExpirationTimeSeconds)
    {
        _cache = cache;
        _expirationTimeSeconds = expirationTimeSeconds;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
    {   
        if (_cache.TryGetValue(key, out var value) &&
            value.CreatedAt >= DateTime.Now.AddSeconds(-_expirationTimeSeconds))
        {
            return (T)value.CachedValue;
        }

        var newValue = await factory();
        _cache.Remove(key);
        _cache.Add(key, (DateTime.Now, newValue!));
        return newValue;
    }
}