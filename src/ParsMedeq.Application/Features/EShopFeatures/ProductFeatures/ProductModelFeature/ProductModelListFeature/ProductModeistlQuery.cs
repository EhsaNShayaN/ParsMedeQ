using EShop.Application.Helpers;
using EShop.Domain.Aggregates.ProductTypeAggregate.Entities;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace EShop.Application.Features.EShopFeatures.ProductFeatures.ProductModelFeature.ProductModelListFeature;
public sealed record ProductModeistlQuery() : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<ProductModel>>;

sealed class InvoiceDraftListByFilterQueryHander : IPrimitiveResultQueryHandler<ProductModeistlQuery, BasePaginatedApiResponse<ProductModel>>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IEShopReadUnitOfWork _eShopReadUnitOfWork;

    public InvoiceDraftListByFilterQueryHander(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IEShopReadUnitOfWork eShopReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._eShopReadUnitOfWork = eShopReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<ProductModel>>> Handle(ProductModeistlQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<BasePaginatedApiResponse<ProductModel>>> HandleCore(
        ProductModeistlQuery request,
        CancellationToken cancellationToken)
    {
        return await this._eShopReadUnitOfWork
        .ProductReadRepository
            .FilterProductModels(request, cancellationToken)
            .ConfigureAwait(false);
    }
}