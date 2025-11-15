using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TreatmentCenterContracts.TreatmentCenterListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.TreatmentCenterFeatures.TreatmentCenterList;

sealed class TreatmentCenterListEndpoint : EndpointHandlerBase<
    TreatmentCenterListApiRequest,
    TreatmentCenterListQuery,
    BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>,
    BasePaginatedApiResponse<TreatmentCenterListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public TreatmentCenterListEndpoint(
        IPresentationMapper<TreatmentCenterListApiRequest, TreatmentCenterListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>, BasePaginatedApiResponse<TreatmentCenterListApiResponse>> responseMapper)
        : base(
            Endpoints.TreatmentCenter.TreatmentCenters,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class TreatmentCenterListApiRequestMapper : IPresentationMapper<
    TreatmentCenterListApiRequest,
    TreatmentCenterListQuery>
{
    public ValueTask<PrimitiveResult<TreatmentCenterListQuery>> Map(
        TreatmentCenterListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new TreatmentCenterListQuery(
                    src.Query,
                    src.ProvinceId,
                    src.CityId)
                {
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class TreatmentCenterListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>,
    BasePaginatedApiResponse<TreatmentCenterListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<TreatmentCenterListApiResponse>>> Map(
        BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<TreatmentCenterListApiResponse>(src.Items.Select(data =>
                    new TreatmentCenterListApiResponse(
                        data.Id,
                        data.ProvinceId,
                        data.CityId,
                        data.Title,
                        data.Description,
                        data.Image,
                        data.CreationDate.ToPersianDate()))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}