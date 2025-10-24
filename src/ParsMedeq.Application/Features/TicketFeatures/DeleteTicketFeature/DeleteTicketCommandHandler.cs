namespace ParsMedeQ.Application.Features.TicketFeatures.DeleteTicketFeature;
public sealed class DeleteTicketCommandHandler : IPrimitiveResultCommandHandler<DeleteTicketCommand, DeleteTicketCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DeleteTicketCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DeleteTicketCommandResponse>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        return await this._writeUnitOfWork.TicketWriteRepository.FindByIdWithAnswers(
            request.Id,
            cancellationToken)
            .Map(ticket => _writeUnitOfWork.TicketWriteRepository.DeleteTicket(ticket)
            .Map(ticket => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None))
            .Map(count => new DeleteTicketCommandResponse(count > 0)))
            .ConfigureAwait(false);
    }
}
