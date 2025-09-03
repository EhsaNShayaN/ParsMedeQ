using ParsMedeQ.Application.Services.UserLangServices;
using ParsMedeQ.Domain;

namespace ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceFeature;
public sealed class UpdateResourceCommandHandler : IPrimitiveResultCommandHandler<UpdateResourceCommand, UpdateResourceCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public UpdateResourceCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<UpdateResourceCommandResponse>> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var langCode = _userLangContextAccessor.GetCurrentLang();
        return
            await this._writeUnitOfWork.ResourceWriteRepository.FindById(request.Id, cancellationToken)
            .MapIf(
                _ => langCode.Equals(Constants.LangCode_Farsi, StringComparison.OrdinalIgnoreCase),
                resource => resource.Update(
                    request.ResourceCategoryId, request.Language, request.PublishDate, request.PublishInfo, request.Publisher,
                    request.Price, request.Discount, request.IsVip, request.ExpirationDate,
                    langCode, request.Title, request.Description, request.Abstract, request.Anchors, request.Keywords),
                resource => resource.UpdateTranslation(langCode, request.Title, request.Description, request.Abstract, request.Anchors, request.Keywords)
                    .Map(() => resource)
            )
            .Map(resource => this._writeUnitOfWork.ResourceWriteRepository.UpdateResource(resource, cancellationToken)
            .Map(resource => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => resource))
            .Map(resource => new UpdateResourceCommandResponse(resource is not null)))
            .ConfigureAwait(false);
    }
}
