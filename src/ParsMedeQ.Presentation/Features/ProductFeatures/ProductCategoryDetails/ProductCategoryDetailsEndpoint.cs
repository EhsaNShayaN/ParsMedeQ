using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ProductCategoryFeatures.ProductCategoryDetailsFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.ProductCategoryDetailsContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ProductCategoryFeatures.ProductCategoryDetails;

sealed class ProductCategoryDetailsEndpoint : EndpointHandlerBase<
    ProductCategoryDetailsApiRequest,
    ProductCategoryDetailsQuery,
    ProductCategoryListDbQueryResponse,
    ProductCategoryDetailsApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public ProductCategoryDetailsEndpoint(
        IPresentationMapper<ProductCategoryDetailsApiRequest, ProductCategoryDetailsQuery> requestMapper,
        IPresentationMapper<ProductCategoryListDbQueryResponse, ProductCategoryDetailsApiResponse> responseMapper)
        : base(
            Endpoints.Product.ProductCategory,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ProductCategoryDetailsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new ProductCategoryDetailsQuery(request.Id))),
            cancellationToken);

}
sealed class ProductCategoryDetailsApiRequestMapper : IPresentationMapper<
    ProductCategoryDetailsApiRequest,
    ProductCategoryDetailsQuery>
{
    public ValueTask<PrimitiveResult<ProductCategoryDetailsQuery>> Map(
        ProductCategoryDetailsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new ProductCategoryDetailsQuery(src.Id)));
    }
}
sealed class ProductCategoryDetailsApiResponseMapper : IPresentationMapper<
    ProductCategoryListDbQueryResponse,
    ProductCategoryDetailsApiResponse>
{
    public ValueTask<PrimitiveResult<ProductCategoryDetailsApiResponse>> Map(
        ProductCategoryListDbQueryResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new ProductCategoryDetailsApiResponse(
                        src.Id,
                        src.Title,
                        src.Description,
                        src.ParentId,
                        src.CreationDate.ToPersianDate())
                    ));
    }
}