using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.CartRepositories;
public interface ICartWriteRepository : IDomainRepository
{
    ValueTask<Cart[]> GetCartsAsync();
    ValueTask<Cart> GetCartAsync(int? userId, Guid? anonymousId);
    ValueTask<Cart> AddToCartAsync(int? userId, Guid? anonymousId, CartItem item);
    ValueTask<bool> CheckoutAsync(int userId);
    ValueTask<Cart> RemoveFromCartAsync(int? userId, Guid? anonymousId, int itemId);
    ValueTask<Cart> MergeCartAsync(Guid anonymousId, int userId);
}
