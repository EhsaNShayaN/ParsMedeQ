using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.SectionFeatures.SectionListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.SectionContracts.AdminSectionListContract;

namespace ParsMedeQ.Presentation.Features.SectionFeatures;

sealed class SectionListEndpoint : EndpointHandlerBase<
    AdminSectionListApiRequest,
    SectionListQuery,
    SectionListDbQueryResponse[],
    AdminSectionListApiResponse[]>
{
    public SectionListEndpoint(
        IPresentationMapper<AdminSectionListApiRequest, SectionListQuery> requestMapper,
        IPresentationMapper<SectionListDbQueryResponse[], AdminSectionListApiResponse[]> responseMapper)
        : base(
            Endpoints.Section.List,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] AdminSectionListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new SectionListQuery())),
            cancellationToken);

}
sealed class AdminSectionListApiRequestMapper : IPresentationMapper<
    AdminSectionListApiRequest,
    SectionListQuery>
{
    public ValueTask<PrimitiveResult<SectionListQuery>> Map(
        AdminSectionListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new SectionListQuery()));
    }
}
sealed class AdminSectionListApiResponseMapper : IPresentationMapper<
    SectionListDbQueryResponse[],
    AdminSectionListApiResponse[]>
{
    public ValueTask<PrimitiveResult<AdminSectionListApiResponse[]>> Map(
        SectionListDbQueryResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                        src.Select(s => new AdminSectionListApiResponse(
                            s.Id,
                            s.SectionId,
                            s.Title,
                            s.Description,
                            s.Image,
                            s.Hidden)).ToArray()));
    }
}