using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductCategoryFeature;
public sealed class AddProductCategoryCommandHandler : IPrimitiveResultCommandHandler<AddProductCategoryCommand, AddProductCategoryCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddProductCategoryCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddProductCategoryCommandResponse>> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
    {
        return await ProductCategory.Create(
            request.ParentId,
            DateTime.Now)
            .Map(ProductCategory => ProductCategory.AddTranslation(Constants.LangCode_Farsi.ToLower(), request.Title, request.Description)
                .Map(() => ProductCategory))
            .Map(ProductCategory => this._writeUnitOfWork.ProductWriteRepository.AddProductCategory(ProductCategory, cancellationToken)
            .Map(ProductCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => ProductCategory))
            .Map(ProductCategory => new AddProductCategoryCommandResponse(ProductCategory is not null)))
            .ConfigureAwait(false);
    }
}
