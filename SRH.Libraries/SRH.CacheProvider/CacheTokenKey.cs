namespace SRH.CacheProvider;

public readonly record struct CacheTokenKey(string Name, CacheToken Parent)
{
    public readonly TimeSpan Expiry => Parent.Expiry;
    public CacheTokenKey(string name) : this(name, CacheToken.Root) { }
    public string GetCacheKey()
    {
        if (Parent.Equals(CacheToken.Root))
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new Exception("The name of CacheTokenKey can not be empty");
            return Name.Trim();
        }
        return $"{Parent.Name}{(string.IsNullOrWhiteSpace(Name) ? string.Empty : $":{Name.Trim()}")}";
    }
}
