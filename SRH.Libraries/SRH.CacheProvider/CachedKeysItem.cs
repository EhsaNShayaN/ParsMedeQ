namespace SRH.CacheProvider;

public readonly record struct CachedKeysItem(string Name, DateTimeOffset Expiry)
{
    public bool IsExpired() => DateTimeOffset.Now > Expiry;
}
