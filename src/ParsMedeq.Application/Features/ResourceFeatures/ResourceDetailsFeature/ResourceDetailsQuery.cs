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
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceDetailsQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
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
        .Map(x => new ResourceDetailsDbQueryResponse())
        .ConfigureAwait(false);

    /*public async Task<PrimitiveResult<Resource>> HandleCore2(
        ResourceDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await ContextualResult<ResourceDetailsQueryHandlerContext>.Create(new(cancellationToken))
            .Execute(ctx => this._readUnitOfWork
                .ResourceReadRepository
                .ResourceDetails(request.ResourceId, ctx.CancellationToken)
                .Map(ctx.SetResource))
            .Execute(ctx => this._readUnitOfWork.ResourceReadRepository.FilterResourceCategories(request.TableId, cancellationToken).Map(ctx.SetResourceCategory))
            .Map(c => c.Resource)
            .ConfigureAwait(false);

        return result;
    }*/
}

/*sealed class ResourceDetailsQueryHandlerContext
{
    private readonly CancellationToken _cancellationToken;

    public Resource Resource { get; private set; } = null!;
    public ResourceCategory[] ResourceCategory { get; private set; } = [];
    public ResourceCategoryRelations[] ResourceCategoryRelations { get; private set; } = [];
    public Purchase? Purchase { get; private set; }
    public CancellationToken CancellationToken => this._cancellationToken;

    public ResourceDetailsQueryHandlerContext(CancellationToken cancellationToken)
    {
        this._cancellationToken = cancellationToken;
    }



    public ResourceDetailsQueryHandlerContext SetResource(Resource val)
    {
        this.Resource = val;
        return this;
    }
    public ResourceDetailsQueryHandlerContext SetResourceCategory(ResourceCategory[] val)
    {
        this.ResourceCategory = val;
        return this;
    }
    public ResourceDetailsQueryHandlerContext SetResourceCategoryRelations(ResourceCategoryRelations[] val)
    {
        this.ResourceCategoryRelations = val;
        return this;
    }
    public ResourceDetailsQueryHandlerContext SetPurchase(Purchase val)
    {
        this.Purchase = val;
        return this;
    }
}*/
