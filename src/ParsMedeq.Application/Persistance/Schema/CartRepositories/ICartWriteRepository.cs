using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.CartRepositories;
public interface ICartWriteRepository : IDomainRepository
{
    ValueTask<Cart> GetCarts(int? userId, Guid anonymousId, string Lang);
    ValueTask<Cart> AddToCart(int? userId, Guid anonymousId, int tableId, int relatedId, int quantity, string Lang);
    ValueTask<bool> CheckoutAsync(int userId);
    ValueTask<Cart> RemoveFromCart(int? userId, Guid anonymousId, int relatedId);
    ValueTask<Cart> MergeCart(int userId, Guid anonymousId);
}
