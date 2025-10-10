namespace ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
public sealed class RemoveFromCartCommandHandler : IPrimitiveResultCommandHandler<RemoveFromCartCommand, RemoveFromCartCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public RemoveFromCartCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<RemoveFromCartCommandResponse>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.RemoveFromCart(
            request.UserId,
            request.AnonymousId,
            request.RelatedId);

        return await this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => cart)
            .Map(cart => new RemoveFromCartCommandResponse(cart is not null))
            .ConfigureAwait(false);
    }
}
