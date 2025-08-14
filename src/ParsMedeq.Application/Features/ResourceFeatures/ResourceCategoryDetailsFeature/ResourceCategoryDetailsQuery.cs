using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceCategoryFeatures.ResourceCategoryDetailsFeature;
public sealed record ResourceCategoryDetailsQuery(
    int Id) : IPrimitiveResultQuery<ResourceCategory>;

sealed class ResourceCategoryDetailsQueryHandler : IPrimitiveResultQueryHandler<ResourceCategoryDetailsQuery, ResourceCategory>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceCategoryDetailsQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceCategory>> Handle(ResourceCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.Id}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ResourceCategory>> HandleCore(
        ResourceCategoryDetailsQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork
            .ResourceReadRepository
            .ResourceCategoryDetails(
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}