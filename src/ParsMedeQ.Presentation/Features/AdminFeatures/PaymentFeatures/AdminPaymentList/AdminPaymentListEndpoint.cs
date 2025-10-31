using ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;
using ParsMedeQ.Domain;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.PaymentFeatures.AdminPaymentList;

sealed class AdminPaymentListEndpoint : EndpointHandlerBase<
    PaymentListApiRequest,
    PaymentListQuery,
    BasePaginatedApiResponse<PaymentListDbQueryResponse>,
    BasePaginatedApiResponse<PaymentListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public AdminPaymentListEndpoint(
        IPresentationMapper<PaymentListApiRequest, PaymentListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<PaymentListDbQueryResponse>, BasePaginatedApiResponse<PaymentListApiResponse>> responseMapper)
        : base(
            Endpoints.Admin.Payments,
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
                new PaymentListQuery(src.RelatedId, true)
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
                        ((PaymentStatus)data.Status).GetDescription(),
                        data.FullName,
                        data.PaidDate,
                        data.CreationDate))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}