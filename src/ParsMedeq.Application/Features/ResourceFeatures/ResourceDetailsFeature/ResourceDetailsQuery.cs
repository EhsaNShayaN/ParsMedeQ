using ParsMedeQ.Application.Services.UserLangServices;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;

public sealed record ResourceDetailsQuery(
    int UserId,
    int ResourceId,
    int TableId) : IPrimitiveResultQuery<ResourceDetailsDbQueryResponse>;

sealed class ResourceDetailsQueryHandler : IPrimitiveResultQueryHandler<ResourceDetailsQuery, ResourceDetailsDbQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceDetailsQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceDetailsDbQueryResponse>> Handle(ResourceDetailsQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.ResourceId}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ResourceDetailsDbQueryResponse>> HandleCore(
        ResourceDetailsQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ResourceReadRepository.ResourceDetails(
            this._userLangContextAccessor.GetCurrentLang(),
            request.UserId,
            request.ResourceId,
            request.TableId,
            cancellationToken)
        .ConfigureAwait(false);
}