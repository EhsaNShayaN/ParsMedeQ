using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate;

namespace ParsMedeQ.Application.Features.OrderFeatures.AddOrderFeature;
public sealed class AddOrderCommandHandler : IPrimitiveResultCommandHandler<AddOrderCommand, AddOrderCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddOrderCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddOrderCommandResponse>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        return await
            _writeUnitOfWork.CartWriteRepository.GetCart(request.CartId, cancellationToken)
            .Map(cart => Order.Create(
            cart.UserId!.Value,
            cart.CartItems.Sum(c => c.UnitPrice * c.Quantity),
            0,
            (byte)OrderStatus.Pending.GetHashCode(),
            $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{cart.Id}")
            .Map(order =>
                PrimitiveResult.BindAll(cart.CartItems, (ci) => order.AddItem(
                    ci.TableId, ci.RelatedId, ci.RelatedName, ci.Quantity, ci.UnitPrice, ci.Quantity * ci.UnitPrice),
                BindAllIterationStrategy.BreakOnFirstError).Map(() => order)
            .Map(order => this._writeUnitOfWork.OrderWriteRepository.AddOrder(order)
            .Map(order => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => order))
            .Map(order => cart.RemoveCartItems().Map(() => order))
            .Map(order => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => order))
            .Map(order => Payment.Create(order.Id, order.FinalAmount!.Value, 0))
            .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.AddPayment(payment)
            .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment))
            .Map(payment => new AddOrderCommandResponse(payment.Id))))))
            .ConfigureAwait(false);
    }
}
