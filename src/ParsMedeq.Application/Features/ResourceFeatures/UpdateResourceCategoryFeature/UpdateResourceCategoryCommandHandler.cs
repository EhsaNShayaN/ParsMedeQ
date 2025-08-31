using ParsMedeQ.Application.Services.UserLangServices;

namespace ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceCategoryFeature;
public sealed class UpdateResourceCategoryCommandHandler : IPrimitiveResultCommandHandler<UpdateResourceCategoryCommand, UpdateResourceCategoryCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public UpdateResourceCategoryCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<UpdateResourceCategoryCommandResponse>> Handle(UpdateResourceCategoryCommand request, CancellationToken cancellationToken)
    {
        return
            await this._writeUnitOfWork.ResourceWriteRepository.FindCategoryById(request.Id, cancellationToken)
            .Map(resourceCategory => resourceCategory.Update(request.ParentId, _userLangContextAccessor.GetCurrentLang(), request.Title, request.Description))
            .Map(resourceCategory => this._writeUnitOfWork.ResourceWriteRepository.UpdateResourceCategory(resourceCategory, cancellationToken)
            .Map(resourceCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => resourceCategory))
            .Map(resourceCategory => new UpdateResourceCategoryCommandResponse(resourceCategory is not null)))
            .ConfigureAwait(false);
    }
}
