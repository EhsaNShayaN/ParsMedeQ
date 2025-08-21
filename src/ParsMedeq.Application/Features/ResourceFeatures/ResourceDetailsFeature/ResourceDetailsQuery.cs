using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;

public sealed record ResourceDetailsQuery(
    int UserId,
    int ResourceId,
    int TableId) : IPrimitiveResultQuery<ResourceDetailsDbQueryResponse>;

sealed class ResourceDetailsQueryHandler : IPrimitiveResultQueryHandler<ResourceDetailsQuery, ResourceDetailsDbQueryResponse>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ResourceDetailsQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<ResourceDetailsDbQueryResponse>> Handle(ResourceDetailsQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.ResourceId}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<ResourceDetailsDbQueryResponse>> HandleCore(
        ResourceDetailsQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork.ResourceReadRepository.ResourceDetails(
            request.UserId,
            request.ResourceId,
            request.TableId,
            cancellationToken)
        .Map(res => new ResourceDetailsDbQueryResponse
        {
            Id = res.Resource.Id,
            TableId = res.Resource.TableId,
            Title = res.ResourceTranslation?.Title ?? string.Empty,
            Description = res.ResourceTranslation?.Description ?? string.Empty,
            Abstract = res.ResourceTranslation?.Abstract ?? string.Empty,
            Anchors = res.ResourceTranslation?.Anchors ?? string.Empty,
            Keywords = res.ResourceTranslation?.Keywords ?? string.Empty,
            ResourceCategoryId = res.Resource.ResourceCategory.Id,
            ResourceCategoryTitle = res.ResourceCategoryTranslation?.Title ?? string.Empty,
            Image = res.Resource.Image,
            FileId = res.Resource.FileId,
            Language = res.Resource.Language,
            PublishDate = res.Resource.PublishDate,
            PublishInfo = res.Resource.PublishInfo,
            Publisher = res.Resource.Publisher,
            Price = res.Resource.Price,
            Discount = res.Resource.Discount,
            IsVip = res.Resource.IsVip,
            DownloadCount = res.Resource.DownloadCount,
            Ordinal = res.Resource.Ordinal,
            Deleted = res.Resource.Deleted,
            Disabled = res.Resource.Disabled,
            ExpirationDate = res.Resource.ExpirationDate,
            CreationDate = res.Resource.CreationDate,
            Registered = res.Resource.Registered,
            /*ResourceCategories = (
                from rel in this.DbContext.ResourceCategoryRelations
                join cat in this.DbContext.ResourceCategory on rel.ResourceCategoryId equals cat.Id
                where rel.Id == res.Resource.Id
                select new ResourceCategoryDbQueryResponse(cat.Id, string.Empty)
            ).ToArray()*/
        })
        .ConfigureAwait(false);
}