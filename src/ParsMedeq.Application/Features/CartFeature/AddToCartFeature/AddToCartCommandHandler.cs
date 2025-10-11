using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Application.Services.UserLangServices;

namespace ParsMedeQ.Application.Features.CartFeature.AddToCartFeature;
public sealed class AddToCartCommandHandler : IPrimitiveResultCommandHandler<AddToCartCommand, AddToCartCommandResponse>
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

    public async Task<PrimitiveResult<AddToCartCommandResponse>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._writeUnitOfWork.CartWriteRepository.AddToCart(
            this._userContextAccessor.GetCurrent().Id.Value,
            request.AnonymousId,
            request.TableId,
            request.RelatedId,
            request.Quantity,
            this._userLangContextAccessor.GetCurrentLang()
        );

        return await this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => cart)
            .Map(cart => new AddToCartCommandResponse(cart is not null))
            .ConfigureAwait(false);
    }
}
