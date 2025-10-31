using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ProvinceAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CartRepositories;
internal sealed class CartWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ICartWriteRepository
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IResourceReadRepository _resourceReadRepository;
    public CartWriteRepository(
        WriteDbContext dbContext,
        IProductReadRepository productReadRepository,
        IResourceReadRepository resourceReadRepository) : base(dbContext)
    {
        this._productReadRepository = productReadRepository;
        this._resourceReadRepository = resourceReadRepository;
    }

    public async ValueTask<PrimitiveResult<Cart>> GetCart(string lang, int id, CancellationToken cancellationToken)
    {
        var cart = await this.DbContext.Cart
        .Include(c => c.CartItems)
        .Where(s => s.Id.Equals(id))
        .FirstOrDefaultAsync(cancellationToken);
        await UpdateCartItems(cart, cancellationToken, lang);

        return cart;
    }

    public async ValueTask<Cart> GetCarts(int? userId, Guid? anonymousId, string lang, CancellationToken cancellationToken)
    {
        var cart = await this.DbContext.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId || c.AnonymousId == anonymousId);

        if (cart == null)
        {
            cart = Cart.Create(userId, anonymousId).Value;
            this.DbContext.Cart.Add(cart);
        }
        await UpdateCartItems(cart, cancellationToken, lang);
        await this.DbContext.SaveChangesAsync(cancellationToken);
        return cart;
    }

    private async Task UpdateCartItems(Cart cart, CancellationToken cancellationToken, string lang)
    {
        foreach (var item in cart.CartItems)
        {
            switch ((Tables)item.TableId)
            {
                case Tables.Product:
                    await this._productReadRepository.ProductDetails(lang, item.RelatedId, cancellationToken)
                        .Map(product => cart.UpdateCartItem(
                            item.TableId,
                            item.RelatedId,
                            product.Title,
                            product.Price,
                            item.Quantity));
                    break;
                case Tables.Article:
                case Tables.News:
                case Tables.Clip:
                    await this._resourceReadRepository.ResourceDetails(lang, item.RelatedId, cancellationToken)
                        .Map(resource => cart.UpdateCartItem(
                            item.TableId,
                            item.RelatedId,
                            resource.Title,
                            resource.Price,
                            item.Quantity));
                    break;
                default:
                    break;
            }
        }

    }

    public async ValueTask<Cart> AddToCart(int? userId, Guid? anonymousId, int tableId, int relatedId, int quantity, string lang, CancellationToken cancellationToken)
    {
        // گرفتن محصول از دیتابیس
        string title = string.Empty;
        decimal price = 0;
        switch ((Tables)tableId)
        {
            case Tables.Product:
                await this._productReadRepository.ProductDetails(lang, relatedId, cancellationToken)
                    .OnSuccess(result =>
                    {
                        var item = result.Value;
                        title = item.Title;
                        price = item.Price;
                    });
                break;
            case Tables.Article:
            case Tables.News:
            case Tables.Clip:
                await this._resourceReadRepository.ResourceDetails(lang, relatedId, cancellationToken)
                    .OnSuccess(result =>
                    {
                        var item = result.Value;
                        title = item.Title;
                        price = item.Price;
                    });
                break;
            default:
                break;
        }

        var cart = await GetCarts(userId, anonymousId, lang, cancellationToken);

        var existingItem = cart.CartItems.FirstOrDefault(i => i.RelatedId == relatedId && i.TableId == tableId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            await cart.AddCartItem(
                tableId,
                relatedId,
                title,
                price,
                quantity);
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
        }

        // سبد بعد از خرید خالی میشه
        this.DbContext.CartItems.RemoveRange(cart.CartItems);
        return true;
    }
    public async ValueTask<Cart> RemoveFromCart(int? userId, Guid? anonymousId, int relatedId, string lang, CancellationToken cancellationToken)
    {
        var cart = await GetCarts(userId, anonymousId, lang, cancellationToken);
        var item = cart.CartItems.FirstOrDefault(i => i.RelatedId == relatedId);
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
