using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.CartRepositories;
public interface ICartWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Cart>> GetCart(int id, CancellationToken cancellationToken);
    ValueTask<Cart> GetCarts(int? userId, Guid? anonymousId, string Lang);
    ValueTask<Cart> AddToCart(int? userId, Guid? anonymousId, int tableId, int relatedId, int quantity, string Lang);
    ValueTask<Cart> RemoveFromCart(int? userId, Guid? anonymousId, int relatedId);
    ValueTask<Cart> MergeCart(int userId, Guid? anonymousId);
}
