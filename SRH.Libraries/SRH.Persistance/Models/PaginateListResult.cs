namespace SRH.Persistance.Models;

public class PaginateListResult<TEntity>
{
    public IEnumerable<TEntity> Data { get; set; } = null!;
    public int Total { get; set; }

    public static PaginateListResult<TEntity> Create(IEnumerable<TEntity>? data, int total) => new()
    {
        Data = data ?? Enumerable.Empty<TEntity>(),
        Total = Math.Max(0, total)
    };
}
