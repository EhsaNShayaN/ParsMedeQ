namespace EShop.Presentation.Abstraction;

interface IPresentationMapper<TSource, TDestination>
{
    ValueTask<PrimitiveResult<TDestination>> Map(TSource src, CancellationToken cancellationToken);
}
