using ParsMedeQ.Application.Helpers;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;
public sealed record ResourceListQuery(int TableId) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<ResourceListDbQueryResponse>>;

sealed class ResourceListQueryHandler : IPrimitiveResultQueryHandler<ResourceListQuery, BasePaginatedApiResponse<ResourceListDbQueryResponse>>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceListQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<ResourceListDbQueryResponse>>> Handle(ResourceListQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.LastId}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<BasePaginatedApiResponse<ResourceListDbQueryResponse>>> HandleCore(
        ResourceListQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ResourceReadRepository.FilterResources(
            request,
            request.LastId,
            request.TableId,
            cancellationToken)
        .ConfigureAwait(false);
}