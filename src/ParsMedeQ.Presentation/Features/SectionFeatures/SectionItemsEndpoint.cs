using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.SectionFeatures.SectionListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.SectionContracts.SectionItemsContract;

namespace ParsMedeQ.Presentation.Features.SectionFeatures;

sealed class SectionItemsEndpoint : EndpointHandlerBase<
    SectionItemsApiRequest,
    SectionItemsQuery,
    SectionListDbQueryResponse[],
    SectionItemsApiResponse[]>
{
    public SectionItemsEndpoint(
        IPresentationMapper<SectionItemsApiRequest, SectionItemsQuery> requestMapper,
        IPresentationMapper<SectionListDbQueryResponse[], SectionItemsApiResponse[]> responseMapper)
        : base(
            Endpoints.Section.Items,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] SectionItemsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new SectionItemsQuery())),
            cancellationToken);

}
sealed class AdminSectionItemsApiRequestMapper : IPresentationMapper<
    SectionItemsApiRequest,
    SectionItemsQuery>
{
    public ValueTask<PrimitiveResult<SectionItemsQuery>> Map(
        SectionItemsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new SectionItemsQuery()));
    }
}
sealed class AdminSectionItemsApiResponseMapper : IPresentationMapper<
    SectionListDbQueryResponse[],
    SectionItemsApiResponse[]>
{
    public ValueTask<PrimitiveResult<SectionItemsApiResponse[]>> Map(
        SectionListDbQueryResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                        src.Select(s => new SectionItemsApiResponse(
                            s.Id,
                            s.SectionId,
                            s.Title,
                            s.Description,
                            s.Image,
                            s.Hidden)).ToArray()));
    }
}