namespace ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
public sealed class MergeCartCommandHandler : IPrimitiveResultCommandHandler<MergeCartCommand, MergeCartCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public MergeCartCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<MergeCartCommandResponse>> Handle(MergeCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.MergeCart(
            request.UserId,
            request.AnonymousId);

        return await this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => cart)
            .Map(cart => new MergeCartCommandResponse(cart is not null))
            .ConfigureAwait(false);
    }
}
