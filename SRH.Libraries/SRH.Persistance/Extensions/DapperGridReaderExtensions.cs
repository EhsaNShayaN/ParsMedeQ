using static Dapper.SqlMapper;

namespace SRH.Persistance.Extensions;

public static class DapperGridReaderExtensions
{
    public static async Task<T[]> ReadOrEmptyAsync<T>(this GridReader src, bool bufferd = false)
    {
        var result = await src.ReadAsync<T>(bufferd).ConfigureAwait(false);
        return result?.ToArray() ?? Array.Empty<T>();
    }

    public static async Task<List<T>> ReadOrEmptyListAsync<T>(this GridReader src, bool bufferd = false)
    {
        var result = await src.ReadAsync<T>(bufferd).ConfigureAwait(false);
        return (result ?? Enumerable.Empty<T>()).ToList();
    }

    public static async Task<T?> ReadOneOrDefaultAsync<T>(this GridReader src, T? defaultValue = default)
    {
        var result = await src.ReadFirstAsync<T>().ConfigureAwait(false);
        return result ?? defaultValue;
    }
}
