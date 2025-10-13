using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CartRepositories;
internal sealed class CartWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ICartWriteRepository
{
    public CartWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public async ValueTask<Cart> GetCarts(int? userId, Guid? anonymousId, string Lang)
    {
        var cart = await this.DbContext.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId || c.AnonymousId == anonymousId);

        if (cart == null)
        {
            cart = Cart.Create(userId, anonymousId).Value;
            this.DbContext.Cart.Add(cart);
        }

        return cart;
    }
    public async ValueTask<Cart> AddToCart(int? userId, Guid? anonymousId, int tableId, int relatedId, int quantity, string Lang)
    {
        // گرفتن محصول از دیتابیس
        var product = await this.DbContext
            .Set<Product>()
            .Include(a => a.ProductTranslations)
            .Where(a => a.Id.Equals(relatedId))
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (product == null)
            throw new InvalidOperationException("محصول پیدا نشد");

        //if (product.Stoc0k <= 0) throw new InvalidOperationException("محصول ناموجود است");

        var cart = await GetCarts(userId, anonymousId, Lang);

        var existingItem = cart.CartItems.FirstOrDefault(i => i.RelatedId == relatedId && i.TableId == tableId);

        if (existingItem != null)
        {
            // چک موجودی
            //if (existingItem.Quantity + quantity > product.Stoc0k) throw new InvalidOperationException($"حداکثر تعداد سفارش {product.Stoc0k} عدد است");

            existingItem.Quantity += quantity;
        }
        else
        {
            //if (quantity > product.Stoc0k) throw new InvalidOperationException($"حداکثر تعداد سفارش {product.Stoc0k} عدد است");

            await cart.AddCartItem(
                tableId,
                relatedId,
                product!.ProductTranslations.First(s => s.LanguageCode.Equals(Lang))!.Title,
                product!.Price ?? 0, quantity);
        }
        return cart;
    }
    public async ValueTask<bool> CheckoutAsync(int userId)
    {
        var cart = await this.DbContext.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null || !cart.CartItems.Any()) return false;

        foreach (var item in cart.CartItems)
        {
            var product = await this.DbContext.Set<Product>().FindAsync(item.RelatedId);
            if (product == null)
                throw new InvalidOperationException($"محصول {item.RelatedName} پیدا نشد");
            //if (product.Stoc0k < item.Quantity) throw new InvalidOperationException($"موجودی محصول {item.RelatedName} کافی نیست");

            //await product.UpdateStoc0k(item.Quantity);
        }

        // سبد بعد از خرید خالی میشه
        this.DbContext.CartItems.RemoveRange(cart.CartItems);
        return true;
    }
    public async ValueTask<Cart> RemoveFromCart(int? userId, Guid? anonymousId, int relatedId)
    {
        var cart = await GetCarts(userId, anonymousId, string.Empty);
        var item = cart.CartItems.FirstOrDefault(i => i.Id == relatedId);
        if (item != null)
        {
            await cart.RemoveCartItem(item);
            this.DbContext.CartItems.Remove(item);
        }

        return cart;
    }
    public async ValueTask<Cart> MergeCart(int userId, Guid? anonymousId)
    {
        var userCart = await this.DbContext.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        var anonCart = await this.DbContext.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.AnonymousId == anonymousId);

        if (anonCart == null) return userCart ?? new Cart(userId);

        if (userCart == null)
        {
            anonCart.UserId = userId;
            anonCart.AnonymousId = null;
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

        return userCart;
    }
}
