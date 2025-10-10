using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.CartFeature.CartListFeature;
public sealed record CartListQuery(
    int? UserId,
    Guid? AnonymousId) : IPrimitiveResultQuery<CartListQueryResponse>;

sealed class GetCartQueryHandler : IPrimitiveResultQueryHandler<CartListQuery, CartListQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public GetCartQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
    }
    public async Task<PrimitiveResult<CartListQueryResponse>> Handle(CartListQuery request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.GetCarts(
            request.UserId,
            request.AnonymousId,
            this._userLangContextAccessor.GetCurrentLang());
        return await this._writeUnitOfWork.SaveChangesAsync(cancellationToken)
            .Map(_ => new CartListQueryResponse(
                cart.Id,
                cart.UserId,
                cart.AnonymousId,
                cart.CartItems.Select(item => new GetCartItemQueryResponse(
                    item.TableId,
                    item.RelatedId,
                    item.RelatedName,
                    item.UnitPrice,
                    item.Quantity)).ToArray()))
            .ConfigureAwait(false);
    }
}