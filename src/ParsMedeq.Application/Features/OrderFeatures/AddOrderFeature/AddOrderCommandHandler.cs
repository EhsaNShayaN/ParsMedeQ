using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;

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
                PrimitiveResult.BindAll(cart.CartItems, (ci) => order.AddItem(ci.TableId, ci.RelatedId, ci.Quantity, ci.UnitPrice, ci.Quantity * ci.UnitPrice),
                BindAllIterationStrategy.BreakOnFirstError).Map(() => order)
            .Map(order => this._writeUnitOfWork.OrderWriteRepository.AddOrder(order)
            .Map(order => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => order))
            .Map(order => cart.RemoveCartItems().Map(() => order))
            .Map(order => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => order))
            .Map(order => new AddOrderCommandResponse(order is not null)))))
            .ConfigureAwait(false);
    }
}
