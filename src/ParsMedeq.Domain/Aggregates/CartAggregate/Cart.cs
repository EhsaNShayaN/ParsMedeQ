using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.CartAggregate;

public sealed class Cart : EntityBase<string>
{
    #region " Fields "
    private List<CartItem> _CartItems = [];
    #endregion

    #region " Properties "
    public string? UserId { get; set; }
    public string? AnonymousId { get; set; }
    #endregion

    #region " Navigation Properties "
    public IReadOnlyCollection<CartItem> CartItems => this._CartItems.AsReadOnly();
    #endregion

    #region " Constructors "
    private Cart() : base(string.Empty) { }
    public Cart(string id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Cart> Create(string userId, string anonymousId) =>
        PrimitiveResult.Success(
            new Cart
            {
                UserId = userId,
                AnonymousId = anonymousId,
            });

    public ValueTask<PrimitiveResult<Cart>> Update(
        string userId)
    {
        this.UserId = userId;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }
    public ValueTask<PrimitiveResult<Cart>> Update(
        string userId,
        string productType,
        int productId,
        string productName,
        decimal unitPrice,
        int quantity)
        => this.Update(userId)
        .Map(_ => this.UpdateCartItem(
            productType,
            productId,
            productName,
            unitPrice,
            quantity)
        .Map(() => this));
    public ValueTask<PrimitiveResult> AddCartItem(
        CartItem item)
        => CartItem.Create(item)
        .OnSuccess(item => this._CartItems.Add(item.Value))
        .Match(
            success => PrimitiveResult.Success(),
            PrimitiveResult.Failure);
    public ValueTask<PrimitiveResult> AddCartItem(
        string productType,
        int productId,
        string productName,
        decimal unitPrice,
        int quantity)
        => CartItem.Create(
            productType,
            productId,
            productName,
            unitPrice,
            quantity)
        .OnSuccess(item => this._CartItems.Add(item.Value))
        .Match(
            success => PrimitiveResult.Success(),
            PrimitiveResult.Failure);

    public ValueTask<PrimitiveResult> UpdateCartItem(
        string productType,
        int productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        var current = _CartItems.FirstOrDefault(s => s.ProductId.Equals(productId));
        if (current is null)
        {
            return this.AddCartItem(
                productType,
                productId,
                productName,
                unitPrice,
                quantity);
        }
        return current.Update(
            productName,
            unitPrice,
            quantity)
            .Match(
            _ => PrimitiveResult.Success(),
            PrimitiveResult.Failure);
    }
    public ValueTask<PrimitiveResult> RemoveCartItem(
        CartItem item)
        => this._CartItems.Remove(item) ? PrimitiveResult.Success() : PrimitiveResult.Failure("","");
    #endregion
}