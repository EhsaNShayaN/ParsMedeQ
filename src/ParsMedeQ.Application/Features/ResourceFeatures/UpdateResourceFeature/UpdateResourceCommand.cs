using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceFeature;

public sealed record class UpdateResourceCommand(
    int Id,
    int TableId,
    string Title,
    string Description,
    string Abstract,
    string Anchors,
    string Keywords,

    int ResourceCategoryId,
    string Language,
    string PublishDate,
    string PublishInfo,
    string Publisher,
    int Price,
    int Discount,
    DateTime? ExpirationDate,
    byte[] Image,
    string ImageExtension,
    byte[] File,
    string FileExtension,
    string OldImagePath,
    int? OldFileId) : IPrimitiveResultCommand<UpdateResourceCommandResponse>,
    IValidatableRequest<UpdateResourceCommand>
{
    public ValueTask<PrimitiveResult<UpdateResourceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Language)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}