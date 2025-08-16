namespace SRH.Persistance.Extensions;

public static class IQueryableExtensions
{
    // LEFT JOIN
    public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IQueryable<TOuter> outer,
        IQueryable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner?, TResult> resultSelector)
        where TInner : class
    {
        return outer
            .GroupJoin(inner, outerKeySelector, innerKeySelector, (o, inners) => new { o, inners })
            .SelectMany(
                x => x.inners.DefaultIfEmpty(),
                (x, i) => resultSelector(x.o, i)
            );
    }

    // RIGHT JOIN (implemented as reversed Left Join)
    public static IEnumerable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(
        this IQueryable<TOuter> outer,
        IQueryable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter?, TInner, TResult> resultSelector)
        where TOuter : class
    {
        return inner
            .GroupJoin(outer, innerKeySelector, outerKeySelector, (i, outers) => new { i, outers })
            .SelectMany(
                x => x.outers.DefaultIfEmpty(),
                (x, o) => resultSelector(o, x.i)
            );
    }
}
