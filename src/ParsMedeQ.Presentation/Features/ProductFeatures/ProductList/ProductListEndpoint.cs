using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ProductFeatures.ProductListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.ProductListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.ProductList;

sealed class ProductListEndpoint : EndpointHandlerBase<
    ProductListApiRequest,
    ProductListQuery,
    BasePaginatedApiResponse<ProductListDbQueryResponse>,
    BasePaginatedApiResponse<ProductListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public ProductListEndpoint(
        IPresentationMapper<ProductListApiRequest, ProductListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<ProductListDbQueryResponse>, BasePaginatedApiResponse<ProductListApiResponse>> responseMapper)
        : base(
            Endpoints.Product.Products,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ProductListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new ProductListQuery(request.TableId)
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                    })),
            cancellationToken);

}
sealed class ProductListApiRequestMapper : IPresentationMapper<
    ProductListApiRequest,
    ProductListQuery>
{
    public ValueTask<PrimitiveResult<ProductListQuery>> Map(
        ProductListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new ProductListQuery(src.TableId)
                {
                    TableId = src.TableId,
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class ProductListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<ProductListDbQueryResponse>,
    BasePaginatedApiResponse<ProductListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<ProductListApiResponse>>> Map(
        BasePaginatedApiResponse<ProductListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<ProductListApiResponse>(src.Items.Select(data =>
                    new ProductListApiResponse(
                        data.Id,
                        data.TableId,
                        data.ProductCategoryId,
                        data.ProductCategoryTitle,
                        data.Title,
                        data.Image,
                        data.Language,
                        data.Price,
                        data.Discount,
                        data.IsVip,
                        data.DownloadCount,
                        data.Ordinal,
                        data.ExpirationDate.HasValue && data.ExpirationDate.Value < DateTime.Now,
                        data.CreationDate.ToPersianDate()))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}