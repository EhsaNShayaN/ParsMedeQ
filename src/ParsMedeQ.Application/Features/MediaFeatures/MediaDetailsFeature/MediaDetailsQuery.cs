using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.MediaCategoryFeatures.MediaDetailsFeature;
public sealed record MediaDetailsQuery(
    int Id) : IPrimitiveResultQuery<Media>;

sealed class MediaDetailsQueryHandler : IPrimitiveResultQueryHandler<MediaDetailsQuery, Media>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public MediaDetailsQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<Media>> Handle(MediaDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork
            .MediaReadRepository
            .FindById(
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}