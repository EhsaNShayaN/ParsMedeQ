using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TreatmentCenterContracts.TreatmentCenterListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.TreatmentCenterFeatures.TreatmentCenterList;

sealed class TreatmentCenterListEndpoint : EndpointHandlerBase<
    TreatmentCenterListApiRequest,
    TreatmentCenterListQuery,
    TreatmentCenterListDbQueryResponse[],
    TreatmentCenterListApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public TreatmentCenterListEndpoint(
        IPresentationMapper<TreatmentCenterListApiRequest, TreatmentCenterListQuery> requestMapper,
        IPresentationMapper<TreatmentCenterListDbQueryResponse[], TreatmentCenterListApiResponse[]> responseMapper)
        : base(
            Endpoints.TreatmentCenter.TreatmentCenters,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] TreatmentCenterListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new TreatmentCenterListQuery())),
            cancellationToken);

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
                new TreatmentCenterListQuery()));
    }
}
sealed class TreatmentCenterListApiResponseMapper : IPresentationMapper<
    TreatmentCenterListDbQueryResponse[],
    TreatmentCenterListApiResponse[]>
{
    public ValueTask<PrimitiveResult<TreatmentCenterListApiResponse[]>> Map(
        TreatmentCenterListDbQueryResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    src.Select(data =>
                    new TreatmentCenterListApiResponse(
                        data.Id,
                        data.ProvinceId,
                        data.CityId,
                        data.Title,
                        data.Description,
                        data.Image,
                        data.CreationDate.ToPersianDate()))
                    .ToArray()));
    }
}