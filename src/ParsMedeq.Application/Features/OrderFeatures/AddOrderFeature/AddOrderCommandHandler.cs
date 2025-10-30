using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Application.Services.UserLangServices;
using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate;

namespace ParsMedeQ.Application.Features.OrderFeatures.AddOrderFeature;
public sealed class AddOrderCommandHandler : IPrimitiveResultCommandHandler<AddOrderCommand, AddOrderCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public AddOrderCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserContextAccessor userContextAccessor,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userContextAccessor = userContextAccessor;
        this._userLangContextAccessor = userLangContextAccessor;
    }

    public async Task<PrimitiveResult<AddOrderCommandResponse>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        return await _writeUnitOfWork.CartWriteRepository.GetCart(_userLangContextAccessor.GetCurrentLang(), request.CartId, cancellationToken)
            .Map(cart => Order.Create(
            cart.UserId!.Value,
            cart.CartItems.Sum(c => c.UnitPrice * c.Quantity),
            0,
            (byte)OrderStatus.Pending.GetHashCode(),
            $"ORD-{DateTime.Now:yyyyMMddHHmmss}-{cart.Id}")
            .Map(order =>
            {
                foreach (var ci in cart.CartItems)
                {
                    DateTime? guarantyExpirationDate = null;
                    int periodicServiceInterval = 0;
                    if (ci.TableId == Tables.Product.GetHashCode())
                    {
                        var product = _writeUnitOfWork.ProductWriteRepository.FindById(ci.RelatedId, cancellationToken).Result.Value;
                        guarantyExpirationDate = product.GuarantyExpirationTime > 0 ? DateTime.Now.AddMonths(product.GuarantyExpirationTime) : null;
                        periodicServiceInterval = product.PeriodicServiceInterval;
                    }
                    order.AddItem(
                    ci.TableId, ci.RelatedId, ci.RelatedName, ci.Quantity, ci.UnitPrice,
                    ci.Quantity * ci.UnitPrice, guarantyExpirationDate, periodicServiceInterval)
                    ;


                    /*   if (periodicServiceInterval > 0)
                   {
                       product.AddPeriodicService(_userContextAccessor.GetCurrent().UserId);
                   }*/
                }
                return order;
            }
            )
            .Map(order => this._writeUnitOfWork.OrderWriteRepository.AddOrder(order)
            .Map(order => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => order))
            .Map(order => cart.RemoveCartItems().Map(() => order))
            .Map(order => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => order))
            .Map(order => Payment.Create(order.Id, order.FinalAmount!.Value, 0))
            .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.AddPayment(payment)
            .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment))
            .Map(payment => new AddOrderCommandResponse(payment.Id)))))
            .ConfigureAwait(false);
    }
}
