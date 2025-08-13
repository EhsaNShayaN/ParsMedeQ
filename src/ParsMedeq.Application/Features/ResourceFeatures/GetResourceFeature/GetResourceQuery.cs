using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;
public sealed record ResourceDetailsQuery(
    int UserId,
    int ResourceId,
    int TableId) : IPrimitiveResultQuery<Resource>;

sealed class ResourceDetailsQueryHandler : IPrimitiveResultQueryHandler<ResourceDetailsQuery, Resource>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceDetailsQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<Resource>> Handle(ResourceDetailsQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.ResourceId}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<Resource>> HandleCore(
        ResourceDetailsQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork
            .ResourceReadRepository
            .ResourceDetails(
                request.ResourceId,
                cancellationToken)
        .Map(resource =>
        this._readUnitOfWork.ResourceReadRepository.FilterResourceCategories(request.TableId, cancellationToken)
        .Map(categories =>
        {
            this._readUnitOfWork.ResourceReadRepository.FilterResourceCategoryRelations(request.ResourceId, cancellationToken)
            .Map(relations => categories.Join(relations, pv => pv.Id, rp => rp.ResourceCategoryId, (pv, rp) => pv).ToArray())
            .Map(categories => resource.ResourceCategories = categories)
            .Map(_ => this._readUnitOfWork.PurchaseReadRepository.GetPurchase(request.TableId, request.ResourceId, request.UserId, cancellationToken))
            .Map(purchase => resource.Registered = purchase is not null);
            return resource;
        }))
        .ConfigureAwait(false);
}