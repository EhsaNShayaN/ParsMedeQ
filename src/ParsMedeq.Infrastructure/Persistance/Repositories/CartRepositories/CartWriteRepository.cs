using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CartRepositories;
internal sealed class CartWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ICartWriteRepository
{
    public CartWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public async ValueTask<Cart> GetCartAsync(string? userId, string? anonymousId)
    {
        var cart = await this.DbContext.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId || c.AnonymousId == anonymousId);

        if (cart == null)
        {
            cart = Cart.Create(userId, anonymousId).Value;
            this.DbContext.Cart.Add(cart);
            await this.DbContext.SaveChangesAsync();
        }

        return cart;
    }
    public async ValueTask<Cart> AddToCartAsync(string? userId, string? anonymousId, CartItem item)
    {
        var cart = await GetCartAsync(userId, anonymousId);

        var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == item.ProductId && i.ProductType == item.ProductType);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            await cart.AddCartItem(item);
        }

        await this.DbContext.SaveChangesAsync();
        return cart;
    }
    public async ValueTask<Cart> RemoveFromCartAsync(string? userId, string? anonymousId, int itemId)
    {
        var cart = await GetCartAsync(userId, anonymousId);
        var item = cart.CartItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            cart.RemoveCartItem(item);
            this.DbContext.CartItems.Remove(item);
        }

        await this.DbContext.SaveChangesAsync();
        return cart;
    }
    public async ValueTask<Cart> MergeCartAsync(string anonymousId, string userId)
    {
        var userCart = await this.DbContext.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        var anonCart = await this.DbContext.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.AnonymousId == anonymousId);

        if (anonCart == null) return userCart ?? new Cart(userId);

        if (userCart == null)
        {
            anonCart.UserId = userId;
            anonCart.AnonymousId = null;
            await this.DbContext.SaveChangesAsync();
            return anonCart;
        }

        // merge items
        foreach (var item in anonCart.CartItems)
        {
            var existing = userCart.CartItems.FirstOrDefault(i => i.ProductId == item.ProductId && i.ProductType == item.ProductType);
            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                userCart.AddCartItem(item);
        }

        this.DbContext.Cart.Remove(anonCart);
        await this.DbContext.SaveChangesAsync();

        return userCart;
    }
    public ValueTask<bool> CheckPriceAsync(Cart cart) => throw new NotImplementedException();
}
