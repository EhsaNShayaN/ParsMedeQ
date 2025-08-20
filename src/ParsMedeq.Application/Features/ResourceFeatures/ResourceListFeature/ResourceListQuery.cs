using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;
public sealed record ResourceListQuery(int TableId) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<Resource>>;

sealed class ResourceListQueryHandler : IPrimitiveResultQueryHandler<ResourceListQuery, BasePaginatedApiResponse<Resource>>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceListQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<Resource>>> Handle(ResourceListQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.LastId}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<BasePaginatedApiResponse<Resource>>> HandleCore(
        ResourceListQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork
            .ResourceReadRepository
            .FilterResources(
                request,
                request.LastId,
                request.TableId,
                cancellationToken)
        .Map(resources => this._readUnitOfWork.ResourceReadRepository.FilterResourceCategories(request.TableId, cancellationToken)
        .Map(categories =>
        {
            foreach (var s in resources.Items)
            {
                //s.ResourceCategoryTitle = categories.FirstOrDefault(f => f.Id == s.ResourceCategoryId)?.Title;
            }
            return resources;
        }))
        .ConfigureAwait(false);
}