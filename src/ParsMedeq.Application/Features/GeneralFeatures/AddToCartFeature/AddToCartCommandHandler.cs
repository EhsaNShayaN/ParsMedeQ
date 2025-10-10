namespace ParsMedeQ.Application.Features.GeneralFeatures.AddToCartFeature;
public sealed class AddToCartCommandHandler : IPrimitiveResultCommandHandler<AddToCartCommand, AddToCartCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddToCartCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddToCartCommandResponse>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(PrimitiveResult.Success(new AddToCartCommandResponse(true)));
        /*return await cartite.Create(
            request.TableId,
            request.Path,
            request.MimeType)
            .Map(media => this._writeUnitOfWork.CartWriteRepository.AddToCartAsync(request.UserId,request.AnonymousId,)
            .Map(media => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => media))
            .Map(media => new AddToCartCommandResponse(media is not null)))
            .ConfigureAwait(false);*/
    }
}
