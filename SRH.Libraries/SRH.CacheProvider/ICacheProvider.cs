namespace SRH.CacheProvider;
public interface ICacheProvider
{
    ValueTask<PrimitiveResult> Set<T>(CacheTokenKey token, T value, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<T>> Get<T>(CacheTokenKey token, T defaultValue, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> Remove(CacheTokenKey token, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<T>> GetOrSet<T>(CacheTokenKey token, Func<ValueTask<PrimitiveResult<T>>> setter, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<CachedKeysItem[]>> Scan(CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> InvalidateCache(string itemKey, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> InvalidateCache(CacheTokenKey token, CancellationToken cancellationToken);
}
