using ParsMedeQ.Application.Services.UserLangServices;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
public sealed record ProductCategoryListQuery() : IPrimitiveResultQuery<ProductCategoryListDbQueryResponse[]>;

sealed class ProductCategoryListQueryHandler : IPrimitiveResultQueryHandler<ProductCategoryListQuery, ProductCategoryListDbQueryResponse[]>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductCategoryListQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ProductCategoryListDbQueryResponse[]>> Handle(ProductCategoryListQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ProductCategoryListDbQueryResponse[]>> HandleCore(
        ProductCategoryListQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ProductReadRepository.FilterProductCategories(
            _userLangContextAccessor.GetCurrentLang(),
            cancellationToken)
        .ConfigureAwait(false);
}