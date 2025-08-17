using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;

public sealed record class AddResourceCommand
(
    int TableId,
    string Title,
    string MimeType,
    string Language,
    bool IsVip,
    int Price,
    int Discount,
    string Description,
    string PublishInfo,
    string Publisher,
    int ResourceCategoryId,
    string ResourceCategoryTitle,
    string Abstract,
    string Anchors,
    DateTime? ExpirationDate,
    string ExpirationTime,
    string Keywords,
    string PublishDate,
    int[] Categories,
    byte[] Image,
    string ImageExtension,
    byte[] File,
    string FileExtension) : IPrimitiveResultCommand<AddResourceCommandResponse>,
    IValidatableRequest<AddResourceCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => MobileType.Create(value.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}