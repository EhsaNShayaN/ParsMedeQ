using ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceCategoryFeatures.ResourceCategoryDetailsFeature;
public sealed record ResourceCategoryDetailsQuery(
    int Id) : IPrimitiveResultQuery<ResourceCategoryListDbQueryResponse>;

sealed class ResourceCategoryDetailsQueryHandler : IPrimitiveResultQueryHandler<ResourceCategoryDetailsQuery, ResourceCategoryListDbQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceCategoryDetailsQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceCategoryListDbQueryResponse>> Handle(ResourceCategoryDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ResourceReadRepository.ResourceCategoryDetails(
            _userLangContextAccessor.GetCurrentLang(),
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}