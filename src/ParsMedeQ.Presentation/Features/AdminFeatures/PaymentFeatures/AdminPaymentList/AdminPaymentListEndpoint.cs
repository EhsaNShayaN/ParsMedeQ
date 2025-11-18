using ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;
using ParsMedeQ.Domain;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.PaymentFeatures.AdminPaymentList;

sealed class AdminPaymentListEndpoint : EndpointHandlerBase<
    AdminPaymentListApiRequest,
    PaymentListQuery,
    BasePaginatedApiResponse<PaymentListDbQueryResponse>,
    BasePaginatedApiResponse<PaymentListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;
    protected override bool NeedAdminPrivilage => true;
    protected override bool NeedAuthentication => true;

    public AdminPaymentListEndpoint(
        IPresentationMapper<AdminPaymentListApiRequest, PaymentListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<PaymentListDbQueryResponse>, BasePaginatedApiResponse<PaymentListApiResponse>> responseMapper)
        : base(
            Endpoints.Admin.Payments,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class AdminPaymentListApiRequestMapper : IPresentationMapper<
    AdminPaymentListApiRequest,
    PaymentListQuery>
{
    public ValueTask<PrimitiveResult<PaymentListQuery>> Map(
        AdminPaymentListApiRequest src,
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