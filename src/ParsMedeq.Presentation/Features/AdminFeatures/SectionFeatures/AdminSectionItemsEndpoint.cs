using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.SectionFeatures.SectionListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.AdminContracts.AdminSectionItemsContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.SectionFeatures;

sealed class AdminSectionItemsEndpoint : EndpointHandlerBase<
    AdminSectionItemsApiRequest,
    SectionItemsQuery,
    SectionListDbQueryResponse[],
    AdminSectionItemsApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;
    protected override bool NeedAdminPrivilage => true;

    public AdminSectionItemsEndpoint(
        IPresentationMapper<AdminSectionItemsApiRequest, SectionItemsQuery> requestMapper,
        IPresentationMapper<SectionListDbQueryResponse[], AdminSectionItemsApiResponse[]> responseMapper)
        : base(
            Endpoints.Admin.SectionItems,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] AdminSectionItemsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new SectionItemsQuery())),
            cancellationToken);

}
sealed class AdminSectionItemsApiRequestMapper : IPresentationMapper<
    AdminSectionItemsApiRequest,
    SectionItemsQuery>
{
    public ValueTask<PrimitiveResult<SectionItemsQuery>> Map(
        AdminSectionItemsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new SectionItemsQuery()));
    }
}
sealed class AdminSectionItemsApiResponseMapper : IPresentationMapper<
    SectionListDbQueryResponse[],
    AdminSectionItemsApiResponse[]>
{
    public ValueTask<PrimitiveResult<AdminSectionItemsApiResponse[]>> Map(
        SectionListDbQueryResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                        src.Select(s => new AdminSectionItemsApiResponse(
                            s.Id,
                            s.SectionId,
                            s.Title,
                            s.Description,
                            s.Image,
                            s.Hidden)).ToArray()));
    }
}