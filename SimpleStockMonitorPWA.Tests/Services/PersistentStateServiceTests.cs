namespace SimpleStockMonitorPWA.Tests.Services;

public class PersistentStateServiceTests
{
    private const string CacheKey = "key";
    private const string OtherCacheKey = "otherKey";
    private PersistentStateService? _sut;

    [Test]
    public async Task Should_return_value_from_cache_if_it_exists()
    {
        // Arrange
        var actionCalled = 0;
        var cache = new Dictionary<string, (DateTime, object)>
        {
            { CacheKey, (DateTime.Now.AddSeconds(-15), 1) },
            { OtherCacheKey, (DateTime.Now.AddSeconds(-15), 2) }
        };
        _sut = new PersistentStateService(cache, expirationTimeSeconds: 30);

        // Act
        var value = await _sut.GetOrCreateAsync(CacheKey,
            () => { actionCalled++; return Task.FromResult(999); });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionCalled, Is.EqualTo(0));
            Assert.That(value, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Should_execute_action_when_value_doesnt_exist_in_cache()
    {
        // Arrange
        var actionCalled = 0;
        var cache = new Dictionary<string, (DateTime, object)>();
        _sut = new PersistentStateService(cache);

        // Act
        var value = await _sut.GetOrCreateAsync(CacheKey,
            () => { actionCalled++; return Task.FromResult(999); });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionCalled, Is.EqualTo(1));
            Assert.That(value, Is.EqualTo(999));
        });
    }

    [Test]
    public async Task Should_execute_action_when_value_exists_in_cache_but_has_expired()
    {
        // Arrange
        var actionCalled = 0;
        var cache = new Dictionary<string, (DateTime, object)>
        {
            { "", (DateTime.Now.AddSeconds(-45), 1) }
        };
        _sut = new PersistentStateService(cache, expirationTimeSeconds: 30);

        // Act
        var value = await _sut.GetOrCreateAsync(CacheKey,
            () => { actionCalled++; return Task.FromResult(999); });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionCalled, Is.EqualTo(1));
            Assert.That(value, Is.EqualTo(999));
        });
    }
}
