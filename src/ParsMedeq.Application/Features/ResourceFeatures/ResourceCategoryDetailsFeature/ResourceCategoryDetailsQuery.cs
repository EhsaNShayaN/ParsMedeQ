using ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;
using ParsMedeQ.Application.Services.UserLangServices;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceCategoryFeatures.ResourceCategoryDetailsFeature;
public sealed record ResourceCategoryDetailsQuery(
    int Id) : IPrimitiveResultQuery<ResourceCategoryListDbQueryResponse>;

sealed class ResourceCategoryDetailsQueryHandler : IPrimitiveResultQueryHandler<ResourceCategoryDetailsQuery, ResourceCategoryListDbQueryResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceCategoryDetailsQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceCategoryListDbQueryResponse>> Handle(ResourceCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.Id}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ResourceCategoryListDbQueryResponse>> HandleCore(
        ResourceCategoryDetailsQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork
            .ResourceReadRepository
            .ResourceCategoryDetails(
            request.Id,
            _userLangContextAccessor.GetCurrentLang(),
            cancellationToken)
        .ConfigureAwait(false);
}