using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceCategoryFeature;
public sealed class AddResourceCategoryCommandHandler : IPrimitiveResultCommandHandler<AddResourceCategoryCommand, AddResourceCategoryCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddResourceCategoryCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddResourceCategoryCommandResponse>> Handle(AddResourceCategoryCommand request, CancellationToken cancellationToken)
    {
        return await ResourceCategory.Create(
            request.Title,
            request.Description,
            request.TableId,
            0,
            request.ParentId,
            DateTime.Now)
            .Map(resourceCategory => resourceCategory.AddTranslation("fa", request.Title, request.Description).Map(() => resourceCategory))
            .Map(resourceCategory => this._writeUnitOfWork.ResourceWriteRepository.AddResourceCategory(resourceCategory, cancellationToken)
            .Map(resourceCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => resourceCategory))
            .Map(resourceCategory => new AddResourceCategoryCommandResponse(resourceCategory is not null)))
            .ConfigureAwait(false);
    }
}
