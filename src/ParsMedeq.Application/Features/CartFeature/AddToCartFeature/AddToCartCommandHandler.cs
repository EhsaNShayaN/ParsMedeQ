using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Application.Services.UserLangServices;

namespace ParsMedeQ.Application.Features.CartFeature.AddToCartFeature;
public sealed class AddToCartCommandHandler : IPrimitiveResultCommandHandler<AddToCartCommand, CartListQueryResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public AddToCartCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserContextAccessor userContextAccessor,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userContextAccessor = userContextAccessor;
        this._userLangContextAccessor = userLangContextAccessor;
    }

    public async Task<PrimitiveResult<CartListQueryResponse>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.AddToCart(
            this._userContextAccessor.GetCurrent().GetUserId(),
            request.AnonymousId,
            request.TableId,
            request.RelatedId,
            request.Quantity,
            this._userLangContextAccessor.GetCurrentLang(),
            cancellationToken);

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
