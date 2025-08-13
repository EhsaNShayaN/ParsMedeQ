using ParsMedeQ.Domain.Aggregates.ResourceAggregate;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;
public sealed class AddResourceCommandHandler : IPrimitiveResultCommandHandler<AddResourceCommand, AddResourceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddResourceCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddResourceCommandResponse>> Handle(AddResourceCommand request, CancellationToken cancellationToken)
    {
        return await Resource.Create(
            request.TableId,
            request.Title,
            request.Abstract,
            request.Anchors,
            request.Description,
            request.Keywords,
            request.ResourceCategoryId,
            request.ResourceCategoryTitle,
            request.Image,
            request.MimeType,
            request.Doc,
            request.Language,
            request.PublishDate,
            request.PublishInfo,
            request.Publisher,
            request.Price,
            request.Discount,
            request.IsVip,
            //request.Ordinal,
            request.ExpirationDate)
                .Map(resource => this._writeUnitOfWork.ResourceWriteRepository
                    .AddResource(resource, cancellationToken)
                    .Map(resource => new AddResourceCommandResponse(resource is not null)))
                .ConfigureAwait(false);
    }
}
