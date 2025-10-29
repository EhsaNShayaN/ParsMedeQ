using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ProductFeatures.ProductDetailsFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.ProductDetailsContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.ProductDetails;

sealed class ProductDetailsEndpoint : EndpointHandlerBase<
    ProductDetailsApiRequest,
    ProductDetailsQuery,
    ProductDetailsDbQueryResponse,
    ProductDetailsApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public ProductDetailsEndpoint(
        IPresentationMapper<ProductDetailsApiRequest, ProductDetailsQuery> requestMapper,
        IPresentationMapper<ProductDetailsDbQueryResponse, ProductDetailsApiResponse> responseMapper)
        : base(
            Endpoints.Product.Product,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ProductDetailsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new ProductDetailsQuery(request.Id))),
            cancellationToken);

}
sealed class ProductDetailsApiRequestMapper : IPresentationMapper<
    ProductDetailsApiRequest,
    ProductDetailsQuery>
{
    public ValueTask<PrimitiveResult<ProductDetailsQuery>> Map(
        ProductDetailsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new ProductDetailsQuery(src.Id)));
    }
}
sealed class ProductDetailsApiResponseMapper : IPresentationMapper<
    ProductDetailsDbQueryResponse,
    ProductDetailsApiResponse>
{
    public ValueTask<PrimitiveResult<ProductDetailsApiResponse>> Map(
        ProductDetailsDbQueryResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new ProductDetailsApiResponse(
                        src.Id,
                        src.ProductCategoryId,
                        src.ProductCategoryTitle,
                        src.Title,
                        src.Description,
                        src.Image,
                        src.FileId,
                        src.Price,
                        src.Discount,
                        src.GuarantyExpirationTime,
                        src.PeriodicServiceInterval,
                        src.Deleted,
                        src.Disabled,
                        src.CreationDate.ToPersianDate(),
                        src.Registered,
                        src.Images.Select(s => new ProductMediaApiResponse
                        {
                            Id = s.Id,
                            Ordinal = s.Ordinal,
                            Path = s.Path
                        }).ToArray())));
    }
}