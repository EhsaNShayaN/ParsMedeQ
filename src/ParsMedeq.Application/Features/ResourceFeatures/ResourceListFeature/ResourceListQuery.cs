using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;
public sealed record ResourceListQuery(int TableId, int? ResourceCategoryId) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<ResourceListDbQueryResponse>>;

sealed class ResourceListQueryHandler : IPrimitiveResultQueryHandler<ResourceListQuery, BasePaginatedApiResponse<ResourceListDbQueryResponse>>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceListQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<ResourceListDbQueryResponse>>> Handle(ResourceListQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ResourceReadRepository.FilterResources(
            request,
            this._userLangContextAccessor.GetCurrentLang(),
            request.TableId,
            request.ResourceCategoryId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
}