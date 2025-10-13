using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
public sealed class RemoveFromCartCommandHandler : IPrimitiveResultCommandHandler<RemoveFromCartCommand, RemoveFromCartCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public RemoveFromCartCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<RemoveFromCartCommandResponse>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.RemoveFromCart(
            this._userContextAccessor.GetCurrent().UserId,
            request.AnonymousId,
            request.RelatedId);

        return await this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => cart)
            .Map(cart => new RemoveFromCartCommandResponse(cart is not null))
            .ConfigureAwait(false);
    }
}
