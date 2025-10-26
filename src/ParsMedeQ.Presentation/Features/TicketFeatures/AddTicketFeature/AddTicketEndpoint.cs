using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.TicketFeatures.AddTicketFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TicketContracts.AddTicketContract;

namespace ParsMedeQ.Presentation.Features.TicketFeatures.AddTicketFeature;
sealed class AddTicketEndpoint : EndpointHandlerBase<
    AddTicketApiRequest,
    AddTicketCommand,
    AddTicketCommandResponse,
    AddTicketApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddTicketEndpoint(
        IPresentationMapper<AddTicketApiRequest, AddTicketCommand> apiRequestMapper
        ) : base(
            Endpoints.Ticket.AddTicket,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddTicketApiRequestMapper : IPresentationMapper<AddTicketApiRequest, AddTicketCommand>
{
    public IFileService _fileService { get; set; }

    public AddTicketApiRequestMapper(IFileService fileService) => this._fileService = fileService;

    public async ValueTask<PrimitiveResult<AddTicketCommand>> Map(AddTicketApiRequest src, CancellationToken cancellationToken)
    {
        var imageInfo = await _fileService.ReadStream(src.Image).ConfigureAwait(false);
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddTicketCommand(
                    src.Title,
                    src.Description,
                    imageInfo)));
    }
}