using ParsMedeQ.Domain.Aggregates.MediaAggregate;

namespace ParsMedeQ.Application.Features.MediaFeatures.AddMediaFeature;
public sealed class AddMediaCommandHandler : IPrimitiveResultCommandHandler<AddMediaCommand, AddMediaCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddMediaCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddMediaCommandResponse>> Handle(AddMediaCommand request, CancellationToken cancellationToken)
    {
        return await Media.Create(
            request.TableId,
            request.Path,
            request.MimeType,
            request.FileName)
            .Map(media => this._writeUnitOfWork.MediaWriteRepository.AddMedia(media)
            .Map(media => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => media))
            .Map(media => new AddMediaCommandResponse(media is not null)))
            .ConfigureAwait(false);
    }
}
