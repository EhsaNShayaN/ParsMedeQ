using ParsMedeQ.Application.Services.UserLangServices;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductFeatures.ProductDetailsFeature;

public sealed record ProductDetailsQuery(
    int UserId,
    int ProductId) : IPrimitiveResultQuery<ProductDetailsDbQueryResponse>;

sealed class ProductDetailsQueryHandler : IPrimitiveResultQueryHandler<ProductDetailsQuery, ProductDetailsDbQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductDetailsQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ProductDetailsDbQueryResponse>> Handle(ProductDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ProductReadRepository.ProductDetails(
            this._userLangContextAccessor.GetCurrentLang(),
            request.UserId,
            request.ProductId,
            cancellationToken)
        .ConfigureAwait(false);
}