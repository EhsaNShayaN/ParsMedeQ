using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.MediaCategoryFeatures.MediaDetailsFeature;
public sealed record MediaDetailsQuery(
    int Id) : IPrimitiveResultQuery<Media>;

sealed class MediaDetailsQueryHandler : IPrimitiveResultQueryHandler<MediaDetailsQuery, Media>
{
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public MediaDetailsQueryHandler(
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<Media>> Handle(MediaDetailsQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}|{request.Id}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<Media>> HandleCore(
        MediaDetailsQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork
            .MediaReadRepository
            .FindById(
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}