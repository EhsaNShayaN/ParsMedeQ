using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.ProductCategoryListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ProductCategoryFeatures.ProductCategoryList;

sealed class ProductCategoryListEndpoint : EndpointHandlerBase<
    ProductCategoryListApiRequest,
    ProductCategoryListQuery,
    ProductCategoryListDbQueryResponse[],
    ProductCategoryListApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public ProductCategoryListEndpoint(
        IPresentationMapper<ProductCategoryListApiRequest, ProductCategoryListQuery> requestMapper,
        IPresentationMapper<ProductCategoryListDbQueryResponse[], ProductCategoryListApiResponse[]> responseMapper)
        : base(
            Endpoints.Product.ProductCategories,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ProductCategoryListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new ProductCategoryListQuery())),
            cancellationToken);

}
sealed class ProductCategoryListApiRequestMapper : IPresentationMapper<
    ProductCategoryListApiRequest,
    ProductCategoryListQuery>
{
    public ValueTask<PrimitiveResult<ProductCategoryListQuery>> Map(
        ProductCategoryListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new ProductCategoryListQuery()));
    }
}
sealed class ProductCategoryListApiResponseMapper : IPresentationMapper<
    ProductCategoryListDbQueryResponse[],
    ProductCategoryListApiResponse[]>
{
    public ValueTask<PrimitiveResult<ProductCategoryListApiResponse[]>> Map(
        ProductCategoryListDbQueryResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    src.Select(data =>
                    new ProductCategoryListApiResponse(
                        data.Id,
                        data.Title,
                        data.Description,
                        data.ParentId,
                        data.CreationDate.ToPersianDate()))
                    .ToArray()));
    }
}