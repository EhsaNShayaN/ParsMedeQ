using ParsMedeQ.Application.Features.OrderFeatures.PeriodicServiceListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.OrderContracts.PeriodicServiceListContract;
using ParsMedeQ.Domain.Helpers;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.OrderFeatures.AdminPeriodicServiceList;

sealed class AdminPeriodicServiceListEndpoint : EndpointHandlerBase<
    PeriodicServiceListApiRequest,
    PeriodicServiceListQuery,
    BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>,
    BasePaginatedApiResponse<PeriodicServiceListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;
    protected override bool NeedAdminPrivilage => true;
    protected override bool NeedAuthentication => true;

    public AdminPeriodicServiceListEndpoint(
        IPresentationMapper<PeriodicServiceListApiRequest, PeriodicServiceListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>, BasePaginatedApiResponse<PeriodicServiceListApiResponse>> responseMapper)
        : base(
            Endpoints.Admin.PeriodicServices,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
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
                new PeriodicServiceListQuery(true)
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
                        HashIdsHelper.HexSerializer.Serialize($"{data.OrderId}|{data.OrderItemId}|{data.Id}"),
                        data.User.FullName.GetValue(),
                        data.RelatedId,
                        data.ServiceDate.ToPersianDate(),
                        data.Done,
                        data.HasNext,
                        data.GuarantyExpirationDate.ToPersianDate()))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}