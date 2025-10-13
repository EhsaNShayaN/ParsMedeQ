using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
public sealed class MergeCartCommandHandler : IPrimitiveResultCommandHandler<MergeCartCommand, MergeCartCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public MergeCartCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<MergeCartCommandResponse>> Handle(MergeCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.MergeCart(
            this._userContextAccessor.GetCurrent().UserId,
            request.AnonymousId);

        return await this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => cart)
            .Map(cart => new MergeCartCommandResponse(cart is not null))
            .ConfigureAwait(false);
    }
}
