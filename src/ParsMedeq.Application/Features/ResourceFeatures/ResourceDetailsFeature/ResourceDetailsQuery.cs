using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;

public sealed record ResourceDetailsQuery(
    int ResourceId) : IPrimitiveResultQuery<ResourceDetailsDbQueryResponse>;

sealed class ResourceDetailsQueryHandler : IPrimitiveResultQueryHandler<ResourceDetailsQuery, ResourceDetailsDbQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceDetailsQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceDetailsDbQueryResponse>> Handle(ResourceDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ResourceReadRepository.ResourceDetails(
            this._userLangContextAccessor.GetCurrentLang(),
            request.ResourceId,
            cancellationToken)
        .ConfigureAwait(false);
}