using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using Order = ParsMedeQ.Domain.Aggregates.OrderAggregate.Order;

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
        return await ContextualResult<AddOrderContext>.Create(
            new AddOrderContext(request, cancellationToken))
            .Execute(GetCart)
            .Execute(CreateOrder)
            .Execute(FindProducts)
            .Execute(AddPeriodicService)
            .Execute(AddOrderItems)
            .Execute(AddOrderToDatabase)
            .Execute(RemoveCartsFromDatabase)
            .Execute(CreatePayment)
            .Execute(AddPaymentToDatabase)
            .Map(context => new AddOrderCommandResponse(context.Payment.Id))
            .ConfigureAwait(false);
    }


    async ValueTask<PrimitiveResult<AddOrderContext>> GetCart(AddOrderContext context)
    {
        return await _writeUnitOfWork.CartWriteRepository
            .GetCart(
                _userLangContextAccessor.GetCurrentLang(),
                context.Request.CartId,
                context.CancellationToken)
            .Map(context.SetCart)
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<AddOrderContext>> CreateOrder(AddOrderContext context)
    {
        return await Order.Create(
            context.Cart.UserId!.Value,
            context.Cart.CartItems.Sum(c => c.UnitPrice * c.Quantity),
            0,
            (byte)OrderStatus.Pending.GetHashCode(),
            $"ORD-{DateTime.Now:yyyyMMddHHmmss}-{context.Cart.Id}")
            .Map(context.SetOrder)
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<AddOrderContext>> FindProducts(AddOrderContext context)
    {
        var productIds = context.GetCartProductIds();
        if (productIds.Length <= 0) return context;

        return await _writeUnitOfWork.ProductWriteRepository.FindByIds(productIds)
            .Map(context.SetProducts)
            .ConfigureAwait(false);
    }
    PrimitiveResult<AddOrderContext> AddPeriodicService(AddOrderContext context)
    {
        if ((context.Order?.OrderItems?.Count ?? 0) == 0) return context;

        PrimitiveResult.BindAll(context.Order.OrderItems.Where(s => s.PeriodicServiceInterval > 0), (orderItem) =>
            orderItem.AddPeriodicService(DateTime.Now),
            BindAllIterationStrategy.BreakOnFirstError);

        return context;
    }
    PrimitiveResult<AddOrderContext> AddOrderItems(AddOrderContext context)
    {
        foreach (var ci in context.Cart.CartItems)
        {
            DateTime? guarantyExpirationDate = null;
            int periodicServiceInterval = 0;

            if (ci.TableId == Tables.Product.GetHashCode())
            {
                var product = context.Products.First(p => p.Id.Equals(ci.RelatedId));
                guarantyExpirationDate = product.GuarantyExpirationTime > 0 ? DateTime.Now.AddMonths(product.GuarantyExpirationTime) : null;
                periodicServiceInterval = product.PeriodicServiceInterval;
            }

            context.Order.AddItem(
                    ci.TableId,
                    ci.RelatedId,
                    ci.RelatedName,
                    ci.Quantity,
                    ci.UnitPrice,
                    ci.Quantity * ci.UnitPrice,
                    guarantyExpirationDate,
                    periodicServiceInterval);

        }

        return context;
    }
    async ValueTask<PrimitiveResult<AddOrderContext>> AddOrderToDatabase(AddOrderContext context)
    {
        return await this._writeUnitOfWork.OrderWriteRepository.AddOrder(context.Order)
            .Map(order => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
            .Map(_ => context))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<AddOrderContext>> RemoveCartsFromDatabase(AddOrderContext context)
    {
        return await context.Cart.RemoveCartItems()
            .Map(() => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
            .Map(_ => context))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<AddOrderContext>> CreatePayment(AddOrderContext context)
    {
        return await Payment.Create(context.Order.Id, context.Order.FinalAmount!.Value, 0)
            .Map(context.SetPayment)
            .ConfigureAwait(false);

    }
    async ValueTask<PrimitiveResult<AddOrderContext>> AddPaymentToDatabase(AddOrderContext context)
    {
        return await this._writeUnitOfWork.PaymentWriteRepository.AddPayment(context.Payment)
            .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
            .Map(_ => context))
            .ConfigureAwait(false);
    }

    sealed class AddOrderContext
    {
        public AddOrderCommand Request { get; }
        public CancellationToken CancellationToken { get; }
        public Cart Cart { get; private set; } = null!;
        public Order Order { get; private set; } = null!;
        public Product[] Products { get; private set; } = [];
        public Payment Payment { get; private set; } = null!;
        public AddOrderContext(AddOrderCommand request, CancellationToken cancellationToken)
        {
            this.Request = request;
            this.CancellationToken = cancellationToken;
        }
        public AddOrderContext SetCart(Cart value)
        {
            this.Cart = value;
            return this;
        }
        public AddOrderContext SetOrder(Order value)
        {
            this.Order = value;
            return this;
        }
        public AddOrderContext SetProducts(Product[] value)
        {
            this.Products = value;
            return this;
        }
        public AddOrderContext SetPayment(Payment value)
        {
            this.Payment = value;
            return this;
        }
        public int[] GetCartProductIds() =>
            this.Cart.CartItems
            .Where(x => x.TableId == Tables.Product.GetHashCode())
            .Select(x => x.RelatedId)
            .ToArray();
    }
}

