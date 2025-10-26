using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;
public sealed record PaymentListQuery(int? RelatedId, bool? IsAdmin) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<PaymentListDbQueryResponse>>;

sealed class PaymentListQueryHandler : IPrimitiveResultQueryHandler<PaymentListQuery, BasePaginatedApiResponse<PaymentListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public PaymentListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<PaymentListDbQueryResponse>>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
    {
        var userId = !request.IsAdmin.HasValue || request.IsAdmin.Value ? 0 : this._userContextAccessor.Current.UserId;
        return await this._readUnitOfWork.PaymentReadRepository.FilterPayments(
            request,
            userId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
    }
}