using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.CartRepositories;
public interface ICartWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Cart>> GetCart(string lang, int id, CancellationToken cancellationToken);
    ValueTask<Cart> GetCarts(int? userId, Guid? anonymousId, string lang, CancellationToken cancellationToken);
    ValueTask<Cart> AddToCart(int? userId, Guid? anonymousId, int tableId, int relatedId, int quantity, string lang, CancellationToken cancellationToken);
    ValueTask<Cart> RemoveFromCart(int? userId, Guid? anonymousId, int relatedId, string lang, CancellationToken cancellationToken);
    ValueTask<Cart> MergeCart(int userId, Guid? anonymousId);
}
