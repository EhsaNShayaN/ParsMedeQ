namespace SRH.CacheProvider;

public readonly record struct CacheToken(string Name, TimeSpan Expiry)
{
    public readonly static CacheToken Root = new CacheToken(string.Empty, TimeSpan.MaxValue);
}
