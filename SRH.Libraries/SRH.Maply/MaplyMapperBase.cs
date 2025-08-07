using Mapster;

namespace SRH.Maply;

public abstract class MaplyMapperBase<TModel, TDto> : IMaplyMapper<TModel, TDto>
    where TModel : class
    where TDto : class
{
    public virtual TDto Map(TModel model) => model.Adapt<TDto>();

}
