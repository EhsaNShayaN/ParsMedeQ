using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.CartAggregate;

public sealed class Cart : EntityBase<int>
{
    #region " Fields "
    private List<CartItem> _CartItems = [];
    #endregion

    #region " Properties "
    public Guid? AnonymousId { get; set; }
    public int? UserId { get; set; }
    #endregion

    #region " Navigation Properties "
    public IReadOnlyCollection<CartItem> CartItems => this._CartItems.AsReadOnly();
    #endregion

    #region " Constructors "
    private Cart() : base(0) { }
    public Cart(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Cart> Create(int? userId, Guid? anonymousId) =>
        PrimitiveResult.Success(
            new Cart
            {
                UserId = userId,
                AnonymousId = anonymousId,
            });

    public ValueTask<PrimitiveResult<Cart>> Update(
        Guid? anonymousId,
        int userId)
    {
        this.AnonymousId = anonymousId;
        this.UserId = userId;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }
    public ValueTask<PrimitiveResult<Cart>> Update(
        Guid? anonymousId,
        int userId,
        int tableId,
        int relatedId,
        string relatedName,
        decimal unitPrice,
        int quantity)
        => this.Update(anonymousId, userId)
        .Map(_ => this.UpdateCartItem(
            tableId,
            relatedId,
            relatedName,
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
        int tableId,
        int relatedtId,
        string relatedName,
        decimal unitPrice,
        int quantity)
        => CartItem.Create(
            tableId,
            relatedtId,
            relatedName,
            unitPrice,
            quantity)
        .OnSuccess(item => this._CartItems.Add(item.Value))
        .Match(
            success => PrimitiveResult.Success(),
            PrimitiveResult.Failure);

    public ValueTask<PrimitiveResult> UpdateCartItem(
        int tableId,
        int relatedId,
        string relatedName,
        decimal unitPrice,
        int quantity)
    {
        var current = _CartItems.FirstOrDefault(s => s.RelatedId.Equals(relatedId));
        if (current is null)
        {
            return this.AddCartItem(
                tableId,
                relatedId,
                relatedName,
                unitPrice,
                quantity);
        }
        return current.Update(
            relatedName,
            unitPrice,
            quantity)
            .Match(
            _ => PrimitiveResult.Success(),
            PrimitiveResult.Failure);
    }
    public ValueTask<PrimitiveResult> RemoveCartItem(
        CartItem item)
    {
        return ValueTask.FromResult(PrimitiveResult.Success());
        //return this._CartItems.Remove(item) ? PrimitiveResult.Success() : PrimitiveResult.Failure("", "");
    }
    #endregion
}