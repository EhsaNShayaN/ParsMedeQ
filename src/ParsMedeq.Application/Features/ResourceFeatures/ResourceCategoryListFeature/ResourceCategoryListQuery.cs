using ParsMedeQ.Application.Services.UserLangServices;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;
public sealed record ResourceCategoryListQuery(int TableId) : IPrimitiveResultQuery<ResourceCategoryListDbQueryResponse[]>;

sealed class ResourceCategoryListQueryHandler : IPrimitiveResultQueryHandler<ResourceCategoryListQuery, ResourceCategoryListDbQueryResponse[]>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceCategoryListQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceCategoryListDbQueryResponse[]>> Handle(ResourceCategoryListQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.TableId}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ResourceCategoryListDbQueryResponse[]>> HandleCore(
        ResourceCategoryListQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ResourceReadRepository.FilterResourceCategories(
            _userLangContextAccessor.GetCurrentLang(),
            request.TableId,
            cancellationToken)
        .ConfigureAwait(false);
}