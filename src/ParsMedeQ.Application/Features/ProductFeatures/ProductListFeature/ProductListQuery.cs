using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductFeatures.ProductListFeature;
public sealed record ProductListQuery(int? ProductCategoryId) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<ProductListDbQueryResponse>>;

sealed class ProductListQueryHandler : IPrimitiveResultQueryHandler<ProductListQuery, BasePaginatedApiResponse<ProductListDbQueryResponse>>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductListQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<ProductListDbQueryResponse>>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    => await this._readUnitOfWork.ProductReadRepository.FilterProducts(
            request,
            this._userLangContextAccessor.GetCurrentLang(),
            request.ProductCategoryId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
}