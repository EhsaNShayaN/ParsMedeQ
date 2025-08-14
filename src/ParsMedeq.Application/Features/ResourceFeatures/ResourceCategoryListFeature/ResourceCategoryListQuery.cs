using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceCategoryFeatures.ResourceCategoryListFeature;
public sealed record ResourceCategoryListQuery(
    int TableId) : BasePaginatedQuery, IPrimitiveResultQuery<ResourceCategory[]>;

sealed class ResourceCategoryListQueryHandler : IPrimitiveResultQueryHandler<ResourceCategoryListQuery, ResourceCategory[]>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceCategoryListQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceCategory[]>> Handle(ResourceCategoryListQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.LastId}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ResourceCategory[]>> HandleCore(
        ResourceCategoryListQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork
            .ResourceReadRepository
            .FilterResourceCategories(
            request.TableId,
            cancellationToken)     
        .ConfigureAwait(false);
}