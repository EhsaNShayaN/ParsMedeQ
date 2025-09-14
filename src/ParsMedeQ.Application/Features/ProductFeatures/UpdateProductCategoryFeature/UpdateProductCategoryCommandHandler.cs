using ParsMedeQ.Application.Services.UserLangServices;
using ParsMedeQ.Domain;

namespace ParsMedeQ.Application.Features.ProductFeatures.UpdateProductCategoryFeature;
public sealed class UpdateProductCategoryCommandHandler : IPrimitiveResultCommandHandler<UpdateProductCategoryCommand, UpdateProductCategoryCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public UpdateProductCategoryCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<UpdateProductCategoryCommandResponse>> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var langCode = _userLangContextAccessor.GetCurrentLang();
        return
            await this._writeUnitOfWork.ProductWriteRepository.FindCategoryById(request.Id, cancellationToken)
            .MapIf(
                _ => langCode.Equals(Constants.LangCode_Farsi, StringComparison.OrdinalIgnoreCase),
                ProductCategory => ProductCategory.Update(request.ParentId, langCode, request.Title, request.Description),
                ProductCategory => ProductCategory.UpdateTranslation(langCode, request.Title, request.Description).Map(() => ProductCategory)
            )
            .Map(ProductCategory => this._writeUnitOfWork.ProductWriteRepository.UpdateProductCategory(ProductCategory, cancellationToken)
            .Map(ProductCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => ProductCategory))
            .Map(ProductCategory => new UpdateProductCategoryCommandResponse(ProductCategory is not null)))
            .ConfigureAwait(false);
    }
}
