using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.TicketAggregate;

namespace ParsMedeQ.Application.Features.TicketFeatures.AddTicketFeature;
public sealed class AddTicketCommandHandler : IPrimitiveResultCommandHandler<AddTicketCommand, AddTicketCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IFileService _fileService;

    public AddTicketCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserContextAccessor userContextAccessor,
        IFileService fileService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userContextAccessor = userContextAccessor;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<AddTicketCommandResponse>> Handle(AddTicketCommand request, CancellationToken cancellationToken)
    {
        return await UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Tickets", cancellationToken)
            .Map(path => Ticket.Create(
            this._userContextAccessor.GetCurrent().UserId,
            request.Title,
            request.Description,
            (byte)TicketStatus.Open.GetHashCode(),
            path)
            .Map(Ticket => this._writeUnitOfWork.TicketWriteRepository.AddTicket(Ticket)
            .Map(Ticket => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => Ticket))
            .Map(Ticket => new AddTicketCommandResponse(Ticket is not null))))
            .ConfigureAwait(false);
    }
    static async ValueTask<PrimitiveResult<string>> UploadFile(
        IFileService fileService,
        byte[] bytes,
        string fileExtension,
        string fodlerName,
        CancellationToken cancellationToken)
    {
        if ((bytes?.Length ?? 0) == 0) return string.Empty;
        var result = await fileService.UploadFile(bytes, fileExtension, fodlerName, cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(result)) return PrimitiveResult.Failure<string>("", "Can not upload file");

        return result;
    }
}
