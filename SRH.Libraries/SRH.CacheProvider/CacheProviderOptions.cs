namespace SRH.CacheProvider;

public sealed class CacheProviderOptions
{
    public string ApplicationName { get; set; } = string.Empty;
    public string ApplicationCacheProviderName { get; set; } = "ApplicationCache";
    public string CachedKeysProviderName { get; set; } = "CachedKeys";
}