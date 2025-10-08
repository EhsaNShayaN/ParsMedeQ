namespace ParsMedeQ.Application.Features.ProductFeatures.DeleteProductCategoryFeature;
public sealed class DeleteProductCategoryCommandHandler : IPrimitiveResultCommandHandler<DeleteProductCategoryCommand, DeleteProductCategoryCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DeleteProductCategoryCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DeleteProductCategoryCommandResponse>> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken) =>
        await _writeUnitOfWork.ProductWriteRepository.FindCategoryWithProducts(request.Id, cancellationToken)
        .MapIf(
        category => category.Products.Count == 0,
        category =>
        {
            _writeUnitOfWork.ProductWriteRepository.DeleteCategory(category, cancellationToken);
            return this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None);
        },
        category => ValueTask.FromResult(PrimitiveResult.Success(-1)))
        .Map(count => new DeleteProductCategoryCommandResponse(count > 0))
        .ConfigureAwait(false);
}
