using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.CartRepositories;
public interface ICartWriteRepository : IDomainRepository
{
    ValueTask<Cart[]> GetCartsAsync();
    ValueTask<Cart> GetCartAsync(string? userId, string? anonymousId);
    ValueTask<Cart> AddToCartAsync(string? userId, string? anonymousId, CartItem item);
    ValueTask<bool> CheckoutAsync(string userId);
    ValueTask<Cart> RemoveFromCartAsync(string? userId, string? anonymousId, int itemId);
    ValueTask<Cart> MergeCartAsync(string anonymousId, string userId);
    ValueTask<bool> CheckPriceAsync(Cart cart);
}
