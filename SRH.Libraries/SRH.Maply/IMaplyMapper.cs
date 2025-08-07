namespace SRH.Maply;

public interface IMapper
{
    TDestionation Map<TSource, TDestionation>(TSource model);
}
public interface IMaplyMapper { }
public interface IMaplyMapper<TModel, TDto> : IMaplyMapper
    where TModel : class
    where TDto : class
{
    TDto Map(TModel model);
}
