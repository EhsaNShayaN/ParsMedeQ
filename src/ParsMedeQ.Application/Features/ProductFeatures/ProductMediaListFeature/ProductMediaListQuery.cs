using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductFeatures.ProductMediaListFeature;
public sealed record ProductMediaListQuery(int ProductId) : BasePaginatedQuery, IPrimitiveResultQuery<ProductMediaListDbQueryResponse[]>;

sealed class ProductMediaListQueryHandler : IPrimitiveResultQueryHandler<ProductMediaListQuery, ProductMediaListDbQueryResponse[]>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductMediaListQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ProductMediaListDbQueryResponse[]>> Handle(ProductMediaListQuery request, CancellationToken cancellationToken)
        => await this._readUnitOfWork.ProductReadRepository.GetProductMediaList(
            request.ProductId,
            cancellationToken)
        .ConfigureAwait(false);
}