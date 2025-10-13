using ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductCategoryFeatures.ProductCategoryDetailsFeature;
public sealed record ProductCategoryDetailsQuery(
    int Id) : IPrimitiveResultQuery<ProductCategoryListDbQueryResponse>;

sealed class ProductCategoryDetailsQueryHandler : IPrimitiveResultQueryHandler<ProductCategoryDetailsQuery, ProductCategoryListDbQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductCategoryDetailsQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ProductCategoryListDbQueryResponse>> Handle(ProductCategoryDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ProductReadRepository.ProductCategoryDetails(
            _userLangContextAccessor.GetCurrentLang(),
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}