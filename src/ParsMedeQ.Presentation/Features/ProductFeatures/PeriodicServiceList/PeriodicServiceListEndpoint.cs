using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ProductFeatures.PeriodicServiceListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.PeriodicServiceListContract;
using ParsMedeQ.Domain;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.PeriodicServiceList;

sealed class PeriodicServiceListEndpoint : EndpointHandlerBase<
    PeriodicServiceListApiRequest,
    PeriodicServiceListQuery,
    BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>,
    BasePaginatedApiResponse<PeriodicServiceListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public PeriodicServiceListEndpoint(
        IPresentationMapper<PeriodicServiceListApiRequest, PeriodicServiceListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>, BasePaginatedApiResponse<PeriodicServiceListApiResponse>> responseMapper)
        : base(
            Endpoints.Product.Products,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] PeriodicServiceListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new PeriodicServiceListQuery()
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                    })),
            cancellationToken);

}
sealed class PeriodicServiceListApiRequestMapper : IPresentationMapper<
    PeriodicServiceListApiRequest,
    PeriodicServiceListQuery>
{
    public ValueTask<PrimitiveResult<PeriodicServiceListQuery>> Map(
        PeriodicServiceListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new PeriodicServiceListQuery()
                {
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class PeriodicServiceListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>,
    BasePaginatedApiResponse<PeriodicServiceListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<PeriodicServiceListApiResponse>>> Map(
        BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<PeriodicServiceListApiResponse>(src.Items.Select(data =>
                    new PeriodicServiceListApiResponse(
                        data.Id,
                        data.UserId,
                        data.User.FullName.GetValue(),
                        data.ProductId,
                        data.Product.ProductTranslations.FirstOrDefault(s => s.LanguageCode == Constants.LangCode_Farsi.ToLower()).Title,
                        data.ServiceDate.ToPersianDate(),
                        data.Done,
                        data.CreationDate.ToPersianDate()))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}