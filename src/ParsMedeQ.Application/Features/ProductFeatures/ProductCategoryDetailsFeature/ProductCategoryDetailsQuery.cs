using ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
using ParsMedeQ.Application.Services.UserLangServices;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductCategoryFeatures.ProductCategoryDetailsFeature;
public sealed record ProductCategoryDetailsQuery(
    int Id) : IPrimitiveResultQuery<ProductCategoryListDbQueryResponse>;

sealed class ProductCategoryDetailsQueryHandler : IPrimitiveResultQueryHandler<ProductCategoryDetailsQuery, ProductCategoryListDbQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductCategoryDetailsQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ProductCategoryListDbQueryResponse>> Handle(ProductCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.Id}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ProductCategoryListDbQueryResponse>> HandleCore(
        ProductCategoryDetailsQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ProductReadRepository.ProductCategoryDetails(
            _userLangContextAccessor.GetCurrentLang(),
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}