using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterDetailsFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TreatmentCenterContracts.TreatmentCenterDetailsContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.TreatmentCenterFeatures.TreatmentCenterDetails;

sealed class TreatmentCenterDetailsEndpoint : EndpointHandlerBase<
    TreatmentCenterDetailsApiRequest,
    TreatmentCenterDetailsQuery,
    TreatmentCenterDetailsDbQueryResponse,
    TreatmentCenterDetailsApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public TreatmentCenterDetailsEndpoint(
        IPresentationMapper<TreatmentCenterDetailsApiRequest, TreatmentCenterDetailsQuery> requestMapper,
        IPresentationMapper<TreatmentCenterDetailsDbQueryResponse, TreatmentCenterDetailsApiResponse> responseMapper)
        : base(
            Endpoints.TreatmentCenter.TreatmentCenter,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] TreatmentCenterDetailsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new TreatmentCenterDetailsQuery(request.Id))),
            cancellationToken);
}
sealed class TreatmentCenterDetailsApiRequestMapper : IPresentationMapper<
    TreatmentCenterDetailsApiRequest,
    TreatmentCenterDetailsQuery>
{
    public ValueTask<PrimitiveResult<TreatmentCenterDetailsQuery>> Map(
        TreatmentCenterDetailsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new TreatmentCenterDetailsQuery(src.Id)));
    }
}
sealed class TreatmentCenterDetailsApiResponseMapper : IPresentationMapper<
    TreatmentCenterDetailsDbQueryResponse,
    TreatmentCenterDetailsApiResponse>
{
    public ValueTask<PrimitiveResult<TreatmentCenterDetailsApiResponse>> Map(
        TreatmentCenterDetailsDbQueryResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new TreatmentCenterDetailsApiResponse(
                        src.Id,
                        src.ProvinceId,
                        src.CityId,
                        src.Title,
                        src.Description,
                        src.Image,
                        src.CreationDate.ToPersianDate())
                    ));
    }
}