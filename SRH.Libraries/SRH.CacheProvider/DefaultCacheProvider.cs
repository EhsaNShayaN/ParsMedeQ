namespace SRH.CacheProvider;

public sealed class DefaultCacheProvider : ICacheProvider
{
    internal const string ServiceKey = "SRHCacheProviderDistributedLockProvider";

    readonly static string _cachedKeysLockKey = $"{Assembly.GetExecutingAssembly().FullName!.Replace(" ", "_")}";

    #region " Fields "
    private readonly IFusionCacheProvider _fusionCacheProvider;
    private readonly IDistributedLockProvider _distributedLockProvider;
    private readonly CacheProviderOptions _opts;
    private readonly IFusionCache _appCache;
    private readonly IFusionCache _keyListCache;
    #endregion

    #region " Constructor "
    public DefaultCacheProvider(
        IFusionCacheProvider fusionCacheProvider,
        [FromKeyedServices(ServiceKey)] IDistributedLockProvider distributedLockProvider,
        IOptions<CacheProviderOptions> opts)
    {
        _fusionCacheProvider = fusionCacheProvider;
        _distributedLockProvider = distributedLockProvider;
        _opts = opts.Value;
        _appCache = _fusionCacheProvider.GetCache(_opts.ApplicationCacheProviderName);
        _keyListCache = _fusionCacheProvider.GetCache(_opts.CachedKeysProviderName);
    }
    #endregion

    #region " Set "
    public async ValueTask<PrimitiveResult> Set<T>(CacheTokenKey token, T value, CancellationToken cancellationToken)
    {
        var itemKey = GetItemCacheKey(token);

        var renewResult = await RenewCachedItemsList(token, itemKey, cancellationToken).ConfigureAwait(false);

        if (renewResult.IsFailure) return renewResult;

        await _appCache.SetAsync(GetItemCacheKey(token), value, token.Expiry, cancellationToken).ConfigureAwait(false);

        return PrimitiveResult.Success();
    }
    #endregion

    #region " Get "
    public async ValueTask<PrimitiveResult<T>> Get<T>(CacheTokenKey token, T defaultValue, CancellationToken cancellationToken)
    {
        var itemKey = GetItemCacheKey(token);

        var result = await _appCache.TryGetAsync<T>(itemKey, token: cancellationToken).ConfigureAwait(false);

        return result.HasValue ? result.Value : defaultValue;
    }
    #endregion

    #region " GetOrSet "
    public async ValueTask<PrimitiveResult<T>> GetOrSet<T>(CacheTokenKey token, Func<ValueTask<PrimitiveResult<T>>> setter, CancellationToken cancellationToken)
    {
        var itemKey = GetItemCacheKey(token);

        var result = await _appCache.TryGetAsync<T>(itemKey, token: cancellationToken).ConfigureAwait(false);

        if (!result.HasValue)
        {
            return await setter.Invoke()
                .Map(settetResult => this.Set(token, settetResult, cancellationToken)
                    .Map(() => settetResult));
        }

        return result.Value;
    }
    #endregion

    public ValueTask<PrimitiveResult> Remove(CacheTokenKey token, CancellationToken cancellationToken) => this.InvalidateCache(token, cancellationToken);

    #region " InvalidateCache "
    public async ValueTask<PrimitiveResult> InvalidateCache(string itemKey, CancellationToken cancellationToken)
    {
        using var lockHandle = await _distributedLockProvider
               .TryAcquireLockAsync(GetCachedItemsListLockKey(), TimeSpan.FromSeconds(2), cancellationToken)
               .ConfigureAwait(false);

        if (lockHandle is null) return PrimitiveResult.Failure("", "Couldn't Acquire Lock");

        var key = GetCachedItemsListCacheKey();

        var currentListValue = await _keyListCache.TryGetAsync<CachedKeysItem[]>(key, token: cancellationToken).ConfigureAwait(false);

        if (currentListValue.HasValue)
        {
            var tmp = currentListValue.Value.ToList();
            tmp.RemoveAll(a => a.Name.Equals(itemKey));
            await _keyListCache.SetAsync(key, tmp.ToArray(), TimeSpan.MaxValue, cancellationToken).ConfigureAwait(false);
        }

        await _appCache.RemoveAsync(itemKey, token: cancellationToken).ConfigureAwait(false);

        return PrimitiveResult.Success();
    }
    public ValueTask<PrimitiveResult> InvalidateCache(CacheTokenKey token, CancellationToken cancellationToken) =>
        InvalidateCache(GetItemCacheKey(token), cancellationToken);
    #endregion

    #region " Scan "
    public async ValueTask<PrimitiveResult<CachedKeysItem[]>> Scan(CancellationToken cancellationToken)
    {
        var key = GetCachedItemsListCacheKey();

        var result = await _keyListCache.TryGetAsync<CachedKeysItem[]>(key, token: cancellationToken).ConfigureAwait(false);

        return result.HasValue ? result.Value : Array.Empty<CachedKeysItem>();
    }
    #endregion

    #region " Private Methods "
    async ValueTask<PrimitiveResult> RenewCachedItemsList(CacheTokenKey token, string newItemkey, CancellationToken cancellationToken)
    {
        var key = GetCachedItemsListCacheKey();

        using var lockHandle = await _distributedLockProvider
            .TryAcquireLockAsync(GetCachedItemsListLockKey(), TimeSpan.FromSeconds(2), cancellationToken)
            .ConfigureAwait(false);

        if (lockHandle is null) return PrimitiveResult.Failure("", "Couldn't Acquire Lock");

        var currentListValue = await _keyListCache.TryGetAsync<CachedKeysItem[]>(key, token: cancellationToken).ConfigureAwait(false);

        var newData = new CachedKeysItem[1];
        var newDataItem = new CachedKeysItem(newItemkey, token.Expiry.Equals(TimeSpan.MaxValue) ? DateTimeOffset.MaxValue : DateTimeOffset.Now.Add(token.Expiry));

        if (currentListValue.HasValue)
        {
            var val = currentListValue.Value;

            var alreadyExistingValue = val.FirstOrDefault(a => a.Name.Equals(newItemkey));
            if (string.IsNullOrWhiteSpace(alreadyExistingValue.Name))
            {
                newData = new CachedKeysItem[val.Length + 1];
                Array.Copy(val, newData, val.Length);
                newData[newData.Length - 1] = newDataItem;
            }
            else
            {
                var tmp = val.ToList();
                tmp.RemoveAll(a => a.Name.Equals(newDataItem.Name));
                tmp.Add(newDataItem);
                newData = tmp.ToArray();
            }
        }
        else
        {
            newData[newData.Length - 1] = newDataItem;
        }

        await _keyListCache.SetAsync(key, newData, TimeSpan.MaxValue, cancellationToken).ConfigureAwait(false);

        return PrimitiveResult.Success();

    }
    string GetItemCacheKey(CacheTokenKey token) => $"{token.GetCacheKey()}";
    string GetCachedItemsListCacheKey() => $"CachedItems";
    string GetCachedItemsListLockKey() => $"{(string.IsNullOrWhiteSpace(_opts.ApplicationName) ? _cachedKeysLockKey : _opts.ApplicationName)}:CachedItemsLock";

    
    #endregion
}
