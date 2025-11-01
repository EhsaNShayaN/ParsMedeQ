using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.LcationFeatures.LcationListFeature;
using ParsMedeQ.Application.Features.ProductFeatures.LocationListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.LocationContracts.LocationListContract;

namespace ParsMedeQ.Presentation.Features.LocationFeatures.LocationList;

sealed class LocationListEndpoint : EndpointHandlerBase<
    LocationListApiRequest,
    LocationListQuery,
    LocationListDbQueryResponse[],
    LocationListApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public LocationListEndpoint(
        IPresentationMapper<LocationListApiRequest, LocationListQuery> requestMapper,
        IPresentationMapper<LocationListDbQueryResponse[], LocationListApiResponse[]> responseMapper)
        : base(
            Endpoints.Location.Locations,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] LocationListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new LocationListQuery())),
            cancellationToken);

}
sealed class LocationListApiRequestMapper : IPresentationMapper<
    LocationListApiRequest,
    LocationListQuery>
{
    public ValueTask<PrimitiveResult<LocationListQuery>> Map(
        LocationListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new LocationListQuery()));
    }
}
sealed class LocationListApiResponseMapper : IPresentationMapper<
    LocationListDbQueryResponse[],
    LocationListApiResponse[]>
{
    public ValueTask<PrimitiveResult<LocationListApiResponse[]>> Map(
        LocationListDbQueryResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    src.Select(data =>
                    new LocationListApiResponse(
                        data.Id,
                        data.ParentId,
                        data.Title))
                    .ToArray()));
    }
}