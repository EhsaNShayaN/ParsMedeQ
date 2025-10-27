using ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;
using ParsMedeQ.Domain;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.UserFeatures.PaymentFeatures.UserPaymentList;

sealed class UserPaymentListEndpoint : EndpointHandlerBase<
    PaymentListApiRequest,
    PaymentListQuery,
    BasePaginatedApiResponse<PaymentListDbQueryResponse>,
    BasePaginatedApiResponse<PaymentListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public UserPaymentListEndpoint(
        IPresentationMapper<PaymentListApiRequest, PaymentListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<PaymentListDbQueryResponse>, BasePaginatedApiResponse<PaymentListApiResponse>> responseMapper)
        : base(
            Endpoints.User.Payments,
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
                new PaymentListQuery(src.RelatedId, false)
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
                        data.PaidDate,
                        data.CreationDate))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}