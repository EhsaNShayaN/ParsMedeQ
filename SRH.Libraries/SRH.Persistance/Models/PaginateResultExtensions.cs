namespace SRH.Persistance.Models;

public static class PaginateResultExtensions
{
    public static PaginateListResult<TResult> MapPaginateResult<TEntity, TResult>(this PaginateListResult<TEntity> src,
        Func<TEntity, TResult> mapper) =>
        PaginateListResult<TResult>.Create(src.Data.Select(mapper), src.Total);
}
