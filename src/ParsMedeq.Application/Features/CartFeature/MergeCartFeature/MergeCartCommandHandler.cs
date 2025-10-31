using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
public sealed class MergeCartCommandHandler : IPrimitiveResultCommandHandler<MergeCartCommand, CartListQueryResponse>
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

    public async Task<PrimitiveResult<CartListQueryResponse>> Handle(MergeCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.MergeCart(
            this._userContextAccessor.GetCurrent().UserId,
            request.AnonymousId);

        return await this._writeUnitOfWork.SaveChangesAsync(cancellationToken)
            .Map(_ => new CartListQueryResponse(
                cart.Id,
                cart.CartItems.Select(item => new GetCartItemQueryResponse(
                    item.TableId,
                    item.RelatedId,
                    item.RelatedName,
                    item.UnitPrice,
                    item.Quantity)).ToArray()))
            .ConfigureAwait(false);
    }
}
