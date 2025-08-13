using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;

public sealed record class AddResourceCommand(
    int Id,
    int TableId,
    string Title,
    string Abstract,
    string Anchors,
    string Description,
    string Keywords,
    int ResourceCategoryId,
    string ResourceCategoryTitle,
    string Image,
    string MimeType,
    string Doc,
    string Language,
    string PublishDate,
    string PublishInfo,
    string Publisher,
    int? Price,
    int? Discount,
    bool IsVip,
    int DownloadCount,
    int? Ordinal,
    bool Deleted,
    bool Disabled,
    DateTime? ExpirationDate,
    DateTime CreationDate
    ) : IPrimitiveResultCommand<AddResourceCommandResponse>,
    IValidatableRequest<AddResourceCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => MobileType.Create(value.Doc)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}