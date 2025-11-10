using ParsMedeQ.Application.Features.TicketFeatures.TicketListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TicketContracts.TicketListContract;

namespace ParsMedeQ.Presentation.Features.TicketFeatures.TicketList;

sealed class TicketListEndpoint : EndpointHandlerBase<
    TicketListApiRequest,
    TicketListQuery,
    BasePaginatedApiResponse<TicketListDbQueryResponse>,
    BasePaginatedApiResponse<TicketListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public TicketListEndpoint(
        IPresentationMapper<TicketListApiRequest, TicketListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<TicketListDbQueryResponse>, BasePaginatedApiResponse<TicketListApiResponse>> responseMapper)
        : base(
            Endpoints.Ticket.Tickets,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class TicketListApiRequestMapper : IPresentationMapper<
    TicketListApiRequest,
    TicketListQuery>
{
    public ValueTask<PrimitiveResult<TicketListQuery>> Map(
        TicketListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new TicketListQuery(src.RelatedId, null)
                {
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class TicketListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<TicketListDbQueryResponse>,
    BasePaginatedApiResponse<TicketListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<TicketListApiResponse>>> Map(
        BasePaginatedApiResponse<TicketListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<TicketListApiResponse>(src.Items.Select(data =>
                    new TicketListApiResponse(
                        data.Id,
                        data.FullName,
                        data.Title,
                        data.Description,
                        data.Status,
                        data.Status.ToString(),
                        data.MediaPath,
                        data.Code,
                        data.CreationDate,
                        data.Answers.Select(answer => new TicketAnswerApiResponse(
                            answer.Id,
                            answer.FullName,
                            answer.Answer,
                            answer.MediaPath,
                            answer.CreationDate)).ToArray()))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}