using ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
public interface IPaymentReadRepository : IDomainRepository
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<PaymentListDbQueryResponse>>> FilterPayments(
        BasePaginatedQuery paginated,
        int userId,
        int lastId,
        CancellationToken cancellationToken);
}
