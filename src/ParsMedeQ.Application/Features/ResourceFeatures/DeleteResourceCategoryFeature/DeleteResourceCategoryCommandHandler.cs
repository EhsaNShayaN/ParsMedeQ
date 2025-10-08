namespace ParsMedeQ.Application.Features.ResourceFeatures.DeleteResourceCategoryFeature;
public sealed class DeleteResourceCategoryCommandHandler : IPrimitiveResultCommandHandler<DeleteResourceCategoryCommand, DeleteResourceCategoryCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DeleteResourceCategoryCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DeleteResourceCategoryCommandResponse>> Handle(DeleteResourceCategoryCommand request, CancellationToken cancellationToken) =>
        await _writeUnitOfWork.ResourceWriteRepository.FindCategoryWithResources(request.Id, cancellationToken)
        .MapIf(
        category => category.Resources.Count == 0,
        category =>
        {
            _writeUnitOfWork.ResourceWriteRepository.DeleteCategory(category, cancellationToken);
            return this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None);
        },
        category => ValueTask.FromResult(PrimitiveResult.Success(-1)))
        .Map(count => new DeleteResourceCategoryCommandResponse(count > 0))
        .ConfigureAwait(false);
}
