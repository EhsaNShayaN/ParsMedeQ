using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ProductFeatures.ProductMediaListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.ProductMediaListContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.ProductMediaList;

sealed class ProductMediaListEndpoint : EndpointHandlerBase<
    ProductMediaListApiRequest,
    ProductMediaListQuery,
    ProductMediaListDbQueryResponse[],
    ProductMediaListApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public ProductMediaListEndpoint(
        IPresentationMapper<ProductMediaListApiRequest, ProductMediaListQuery> requestMapper,
        IPresentationMapper<ProductMediaListDbQueryResponse[], ProductMediaListApiResponse[]> responseMapper)
        : base(
            Endpoints.Product.ProductMediaList,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ProductMediaListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new ProductMediaListQuery(request.ProductId))),
            cancellationToken);

}
sealed class ProductMediaListApiRequestMapper : IPresentationMapper<
    ProductMediaListApiRequest,
    ProductMediaListQuery>
{
    public ValueTask<PrimitiveResult<ProductMediaListQuery>> Map(
        ProductMediaListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new ProductMediaListQuery(src.ProductId)));
    }
}
sealed class ProductMediaListApiResponseMapper : IPresentationMapper<
    ProductMediaListDbQueryResponse[],
    ProductMediaListApiResponse[]>
{
    public ValueTask<PrimitiveResult<ProductMediaListApiResponse[]>> Map(
        ProductMediaListDbQueryResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    src.Select(data =>
                    new ProductMediaListApiResponse(
                        data.Id,
                        data.ProductId,
                        data.MediaId,
                        data.Ordinal,
                        data.Path))
                    .ToArray()));
    }
}