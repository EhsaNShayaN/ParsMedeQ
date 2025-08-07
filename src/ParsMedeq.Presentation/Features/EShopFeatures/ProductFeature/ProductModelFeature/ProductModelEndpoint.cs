using EShop.Application.Features.EShopFeatures.ProductFeatures.ProductModelFeature.ProductModelListFeature;
using EShop.Application.Helpers;
using EShop.Contracts;
using EShop.Contracts.ProcuctContract.ProductModel;
using EShop.Domain.Aggregates.ProductTypeAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EShop.Presentation.Features.EShopFeatures.ProductFeature.ProductModelFeature;
sealed class ProductModelEndpoint : EndpointHandlerBase<
    ProductModelApiRequest,
    ProductModeistlQuery,
    BasePaginatedApiResponse<ProductModel>,
    BasePaginatedApiResponse<ProductModel>>
{
    protected override bool NeedTaxPayerFile => true;

    public ProductModelEndpoint(
        IPresentationMapper<ProductModelApiRequest, ProductModeistlQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<ProductModel>, BasePaginatedApiResponse<ProductModel>> responseMapper)
        : base(
            EShopEndpoints.Product.ProductModels,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ProductModelApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new ProductModeistlQuery()
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        LastId = request.LastId,
                    })),
            cancellationToken);

}


sealed class ProductModelApiRequestMapper : IPresentationMapper<
    ProductModelApiRequest,
    ProductModeistlQuery>
{
    public ValueTask<PrimitiveResult<ProductModeistlQuery>> Map(
        ProductModelApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(new ProductModeistlQuery()
            {
                PageIndex = src.PageIndex,
                PageSize = src.PageSize,
                LastId = src.LastId
            }));
    }
}

sealed class ProductModelMApper : IPresentationMapper<
    BasePaginatedApiResponse<ProductModel>,
    BasePaginatedApiResponse<ProductModel>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<ProductModel>>> Map(
        BasePaginatedApiResponse<ProductModel> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<ProductModel>(src.Items, src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}