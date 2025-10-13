using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;
public sealed record ResourceCategoryListQuery(int TableId) : IPrimitiveResultQuery<ResourceCategoryListDbQueryResponse[]>;

sealed class ResourceCategoryListQueryHandler : IPrimitiveResultQueryHandler<ResourceCategoryListQuery, ResourceCategoryListDbQueryResponse[]>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceCategoryListQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceCategoryListDbQueryResponse[]>> Handle(ResourceCategoryListQuery request, CancellationToken cancellationToken)
    {
        return await this._readUnitOfWork.ResourceReadRepository.FilterResourceCategories(
            _userLangContextAccessor.GetCurrentLang(),
            request.TableId,
            cancellationToken)
            .ConfigureAwait(false);
    }
}