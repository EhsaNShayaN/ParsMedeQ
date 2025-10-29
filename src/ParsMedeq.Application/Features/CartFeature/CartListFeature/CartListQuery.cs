using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.CartFeature.CartListFeature;
public sealed record CartListQuery(Guid? AnonymousId) : IPrimitiveResultQuery<CartListQueryResponse>;

sealed class GetCartQueryHandler : IPrimitiveResultQueryHandler<CartListQuery, CartListQueryResponse>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public GetCartQueryHandler(
        IUserContextAccessor userContextAccessor,
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<CartListQueryResponse>> Handle(CartListQuery request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.GetCarts(
            this._userContextAccessor.GetCurrent().GetUserId(),
            request.AnonymousId,
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
                    item.Quantity,
                    item.Data)).ToArray()))
            .ConfigureAwait(false);
    }
}