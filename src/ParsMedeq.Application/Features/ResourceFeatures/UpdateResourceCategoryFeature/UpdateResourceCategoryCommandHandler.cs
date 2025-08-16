namespace ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceCategoryFeature;
public sealed class UpdateResourceCategoryCommandHandler : IPrimitiveResultCommandHandler<UpdateResourceCategoryCommand, UpdateResourceCategoryCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public UpdateResourceCategoryCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<UpdateResourceCategoryCommandResponse>> Handle(UpdateResourceCategoryCommand request, CancellationToken cancellationToken)
    {
        return
            await this._writeUnitOfWork.ResourceWriteRepository.FindCategoryById(request.Id, cancellationToken)
            .Map(resourceCategory => resourceCategory.Update(request.Title, request.Description, request.ParentId))
            .Map(resourceCategory => this._writeUnitOfWork.ResourceWriteRepository.UpdateResourceCategory(resourceCategory, cancellationToken)
            .Map(resourceCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => resourceCategory))
            .Map(resourceCategory => new UpdateResourceCategoryCommandResponse(resourceCategory is not null)))
            .ConfigureAwait(false);
    }
}
