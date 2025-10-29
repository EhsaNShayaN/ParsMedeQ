using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Application.Services.UserLangServices;

namespace ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
public sealed class RemoveFromCartCommandHandler : IPrimitiveResultCommandHandler<RemoveFromCartCommand, CartListQueryResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public RemoveFromCartCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserContextAccessor userContextAccessor,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userLangContextAccessor = userLangContextAccessor;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<CartListQueryResponse>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.RemoveFromCart(
            this._userContextAccessor.GetCurrent().UserId,
            request.AnonymousId,
            request.RelatedId,
            _userLangContextAccessor.GetCurrentLang(),
            cancellationToken);

        return await this._writeUnitOfWork.SaveChangesAsync(cancellationToken)
            .Map(_ => new CartListQueryResponse(
                cart.Id,
                cart.CartItems.Select(item => new GetCartItemQueryResponse(
                    item.TableId,
                    item.RelatedId,
                    item.RelatedName,
                    item.UnitPrice,
                    item.Quantity,
                    item.Data)).ToArray()))
            .ConfigureAwait(false);
    }
}
