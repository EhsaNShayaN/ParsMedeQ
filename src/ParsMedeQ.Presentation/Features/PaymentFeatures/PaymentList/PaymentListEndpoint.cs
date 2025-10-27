using ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;

namespace ParsMedeQ.Presentation.Features.PaymentFeatures.PaymentList;

sealed class PaymentListEndpoint : EndpointHandlerBase<
    PaymentListApiRequest,
    PaymentListQuery,
    BasePaginatedApiResponse<PaymentListDbQueryResponse>,
    BasePaginatedApiResponse<PaymentListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public PaymentListEndpoint(
        IPresentationMapper<PaymentListApiRequest, PaymentListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<PaymentListDbQueryResponse>, BasePaginatedApiResponse<PaymentListApiResponse>> responseMapper)
        : base(
            Endpoints.Payment.Payments,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class PaymentListApiRequestMapper : IPresentationMapper<
    PaymentListApiRequest,
    PaymentListQuery>
{
    public ValueTask<PrimitiveResult<PaymentListQuery>> Map(
        PaymentListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new PaymentListQuery(src.RelatedId, null)
                {
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class PaymentListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<PaymentListDbQueryResponse>,
    BasePaginatedApiResponse<PaymentListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<PaymentListApiResponse>>> Map(
        BasePaginatedApiResponse<PaymentListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<PaymentListApiResponse>(src.Items.Select(data =>
                    new PaymentListApiResponse(
                        data.Id,
                        data.Amount,
                        data.PaymentMethod,
                        data.TransactionId,
                        data.Status,
                        data.PaidDate,
                        data.CreationDate))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}