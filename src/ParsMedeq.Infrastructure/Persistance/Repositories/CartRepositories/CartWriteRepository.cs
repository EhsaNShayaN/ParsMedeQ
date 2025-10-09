using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CartRepositories;
internal sealed class CartWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ICartWriteRepository
{
    public CartWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public async ValueTask<Cart[]> GetCartsAsync()
    {
        return await this.DbContext.Cart.Include(c => c.CartItems).ToArrayAsync();
    }
    public async ValueTask<Cart> GetCartAsync(int? userId, Guid? anonymousId)
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
    public async ValueTask<Cart> AddToCartAsync(int? userId, Guid? anonymousId, CartItem item)
    {
        // گرفتن محصول از دیتابیس
        var product = await this.DbContext.Set<Product>().FindAsync(item.RelatedId);
        if (product == null)
            throw new InvalidOperationException("محصول پیدا نشد");

        if (product.Stock <= 0)
            throw new InvalidOperationException("محصول ناموجود است");

        var cart = await GetCartAsync(userId, anonymousId);

        var existingItem = cart.CartItems.FirstOrDefault(i => i.RelatedId == item.RelatedId && i.TableId == item.TableId);

        if (existingItem != null)
        {
            // چک موجودی
            if (existingItem.Quantity + item.Quantity > product.Stock)
                throw new InvalidOperationException($"حداکثر تعداد سفارش {product.Stock} عدد است");

            existingItem.Quantity += item.Quantity;
        }
        else
        {
            if (item.Quantity > product.Stock)
                throw new InvalidOperationException($"حداکثر تعداد سفارش {product.Stock} عدد است");

            await cart.AddCartItem(item);
        }

        await this.DbContext.SaveChangesAsync();
        return cart;
    }
    public async ValueTask<bool> CheckoutAsync(int userId)
    {
        var cart = await this.DbContext.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null || !cart.CartItems.Any()) return false;

        foreach (var item in cart.CartItems)
        {
            var product = await this.DbContext.Set<Product>().FindAsync(item.RelatedId);
            if (product == null || product.Stock < item.Quantity)
                throw new InvalidOperationException($"موجودی محصول {item.RelatedName} کافی نیست");

            await product.UpdateStock(item.Quantity);
        }

        // سبد بعد از خرید خالی میشه
        this.DbContext.CartItems.RemoveRange(cart.CartItems);
        await this.DbContext.SaveChangesAsync();

        return true;
    }
    public async ValueTask<Cart> RemoveFromCartAsync(int? userId, Guid? anonymousId, int itemId)
    {
        var cart = await GetCartAsync(userId, anonymousId);
        var item = cart.CartItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            await cart.RemoveCartItem(item);
            this.DbContext.CartItems.Remove(item);
        }

        await this.DbContext.SaveChangesAsync();
        return cart;
    }
    public async ValueTask<Cart> MergeCartAsync(Guid anonymousId, int userId)
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
            var existing = userCart.CartItems.FirstOrDefault(i => i.RelatedId == item.RelatedId && i.TableId == item.TableId);
            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                userCart.AddCartItem(item);
        }

        this.DbContext.Cart.Remove(anonCart);
        await this.DbContext.SaveChangesAsync();

        return userCart;
    }
}
